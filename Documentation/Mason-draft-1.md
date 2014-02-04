**PLEASE DO NOT EDIT THIS AS IT WILL BE REPLACED FROM OTHER SOURCE**


## Introduction

Mason is a JSON format for adding hypermedia elements, standardized error handling and additional meta data to classic JSON representations. It is a generic format and imposes very little restrictions on the data it is integrated with.

> Note: The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](http://tools.ietf.org/html/rfc2119).

A Mason document is constructed by taking a "classic" JSON object and then merging hypermedia elements and other Mason features into it. Mason properties are prefixed with `@` to avoid collisions with existing property names.

Here is a simple example to introduce the format. It represents a single issue from an issue tracker application:

```json
{
  // Classic API data
  "ID": 1,
  "Title": "Program crashes when pressing ctrl-p",
  "Description": "I pressed ctrl-p and, boom, it crashed.",
  "Severity": 5,
  "Attachments": [
    {
      "Id": 1,
      "Title": "Error report",
      // Hypermedia linking to attachment
      "@links": {
        "self": {
          "href": "http://issue-tracker.org/attachments/1"
        }
      }
    }
  ],
  // Additional hypermedia elements
  "@links": {
    // Hypermedia linking to self
    "self": {
      "href": "http://issue-tracker.org/issues/1"
    },
    // Hypermedia linking to containing project
    "up": {
      "href": "http://issue-tracker.org/projects/1",
      "title": "Containing project"
    },
  }
}
```

## Syntax

Mason is based on JSON and follows as such all the syntax rules for valid JSON documents. Mason reserves the character '@' as a prefix for Mason property names.

The prefix character is not used for all Mason properties, only for those that co-exists with properties from the underlying resource data - for instance '@meta' and '@actions'. Other property names like "template" and "parameters" are used only in contexts where it is not allowed to mix data in so these properties do not use the '@' prefix.


## Curies

Curie is an abbreviation for "Compact URI" and is a way to define short scoped names that map to URIs. Mason uses namespace declarations to declare prefixes for use in Curies. A Curie is expanded to a URI by replacing the namespace prefix with the corresponding name declared in the `@namespaces` object.

See [CURIE Syntax 1.0](http://www.w3.org/TR/2009/CR-curie-20090116/) for further information.


## Minimized responses

Some of the Mason elements are only needed for client developers exploring the API - for instance the @meta element and "title" property of links. These elements can be removed by the server at runtime to reduce the size of the payload and thus save some bandwith.

The recommended way of instructing the server to return a minimal response is to pass the value "representation=minimal" in the ["Prefer" header](http://tools.ietf.org/html/draft-snell-http-prefer-18).


## Reserved property names

This is the complete list of reserved property names and their semantics. Mason may add new properties prefixed with `@` in future versions and clients must be prepared for this. Unknown Mason properties should be ignored by clients.


### `@meta`

The `@meta` property is OPTIONAL. If present it MUST be an object value. It can only be present in the root object.

The meta object contains information targeted at client developers exploring or debugging the API. The intention of the meta information is to document and highlight details about the API but it is not restricted to this use.

The meta object can be extended with additional application specific properties and thus the standard Mason meta properties must be prefixed with '@'.

**Example usage of `@meta`**

```json
"@meta": {
    "@title": "Issue",
    "@description": "This resource represents a single issue with its data and related actions.",
    "@links": {
        "terms-of-service": {
            "href": "...",
            "title": "Terms of service"
        }
    }
}
```

The meta object can safely be removed in minimized representations. Clients can safely ignore it if present.

#### Properties for `@meta`

##### `@title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a descriptive title.

##### `@description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text.

##### `@links` (optional)
This property is OPTIONAL. If present it MUST be an object with links. It defines links to other resources that are relevant for client developers - for instance API documentation or terms of service.


### `@namespaces`

The `@namespaces` property is OPTIONAL. If present it MUST be an object value. It can only occur in the root object (Mason does not support nested namespace declarations).

The namespaces object contains a set of namespace objects indexed by their namespace prefix. Each namespace object defines the namespace URI using the property `name`.

Namespaces are used to expand curies in link relations, link templates and action names.

The namespace object is not extendable and thus its property names need not be prefixed with '@'.

**Example usage of `@namespaces`**

```json
"@namespaces": {
  "is": {
    "name": "http://issue-tracker-reltypes.org/rels#"
  }
}
```

The namespace object must not be removed in minimized representations. Clients must respect the rules for managing namespaces and curies.

#### Properties for `@namespaces`

##### `<prefix>` (property name)
Property names define namespace prefix.

##### `name`
This property is REQUIRED and MUST be a string value. It contains the URI for the namespace.


### `@links`

The `@links` property is OPTIONAL. If present it MUST be an object value. It is not restricted to the root object and may occur in any nested data object.

Links represents a relationship between one resource and another as described in [RFC 5988 Web Linking](http://tools.ietf.org/search/rfc5988). The relationship between the two resources is assigned a name (relationship type) which is used by the client to locate the link in the Mason document.

The link object contains a set of link objects indexed by their relationship type. The relationship type can either be a simple predefined token from the [IANA relationship registry](http://www.iana.org/assignments/link-relations/link-relations.xhtml), a curie or a complete URI.

One single link relation may have multiple links with different types. In this case the additional links are stored in the `alt` property as an array of additional link objects. The singular "top" link should be the link expected to be used most - the additional links can be seen as alternative links. This makes it simple for clients to access the most used link and if they are aware of possible alternatives then they can search the `alt` links for media type hints that fit their task at hand better.

A link object is not extendable and thus its property names need not be prefixed with '@'.

**Example usage of `@links`**

```json
"@links": {
  // Standard IANA link
  "self": {
    "href": "..."
  },

  // Non standard link identified by a curie
  "is:contact": {
    "href": "...",
    "title": "Complete contact information in standard formats such as vCard and jCard"
  },

  // Non standard link identified by a complete URI
  "http://issue-tracker-reltypes.org/rels#logo": {
    "href": "...",
    "title": "Image of the logo for this instance of issue tracker.",
    "type": "image/png"
  },

  // Link with alternate URLs for other types
  "is:contact": {
    "href": "...",
    "title": "Contact information as vCard",
    "type": "text/vcard",
    "alt": [
      {
        "href": "...",
        "title": "Contact information as jCard",
        "type": "application/vcard+json"
      }
    ]
  }
}
```

The links object itself cannot be removed in minimized representations. Some of the link properties may although be removed as described below.

#### Properties for `@links`

##### `<rel-type>` (property name)
Property names define the link relationship type.

##### `href`
This property is REQUIRED and MUST be a string value representing a valid URI. It contains the target URI of the link.

##### `title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short descriptive title for the action.

This property can safely be removed in minimized representations.

##### `type` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains the expected media type of the target resource.

##### `alt` (optional)
This property is OPTIONAL. If present it MUST be an array of link objects. It contains additional links for the same relation ship types. These links must have different `type` values.


### `@link-templates`

The `@link-templates` property is OPTIONAL. If present it MUST be an object. It is not restricted to the root object and may occur in any nested data object.

The link-templates object contains a set of link-template objects indexed by their name.

A link template object is not extendable and thus its property names need not be prefixed with '@'.


**Example usage of `@link-templates`**

```json
"@link-templates": {
  "is:issue-query": {
    "template": "http://.../issues-query?text={text}&severity={severity}&project={pid}",
    "title": "Search for issues",
    "description": "This is a simple search that does not check attachments.",
    "parameters": [
      {
        "name": "text",
        "description": "Substring search for text in title and description"
      },
      {
        "name": "severity",
        "description": "Issue severity (exact value, 1..5)"
      },
      {
        "name": "pid",
        "description": "Project ID"
      }
    ]
  }
}
```

The link-templates object itself cannot be removed in minimized representations. Some of the link-template properties may although be removed as described below.

#### Properties for `@link-templates`

##### `<name>` (property name)
Property names define the link template name.

##### `template`
This property is REQUIRED and MUST be a string value representing a valid URL template according to [RFC 6570](http://tools.ietf.org/html/rfc6570).

##### `title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short descriptive title for the action.

This property can safely be removed in minimized representations.

##### `description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains some descriptive text for the action.

This property can safely be removed in minimized representations.

##### `parameters` (optional)
This property is OPTIONAL. If present it MUST be an array of parameter definition objects as described below.

It can safely be removed in minimized representations.

#### Template parameters

Each entry in the `parameters` property defines a parameter variable of the template.

##### `parameters[].name`
This property is REQUIRED and MUST be a string. It defines the name of the parameter.

##### `parameters[].description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text for the parameter.

This property can safely be removed in minimized representations.


### `@actions`

The `@actions` property is OPTIONAL. If present it MUST be an object. It is not restricted to the root object and may occur in any nested data object.

Mason features a set of different action types with different payloads. It has "void" for actions without a payload, "json" for structured JSON data, "json+files" for upload of files plus structured JSON data and "any" for a payload of any media type.

An action object is not extendable and thus its property names need not be prefixed with '@'.

The actions object itself cannot be removed in minimized representations. Some of the action properties may although be removed as described below.

#### Action types

##### Action type `void`
Void actions are for use with HTTP methods that carries no payload - for instance DELETE or POST (but not restricted to these).

**Example usage of `void` action**

```json
"@actions": {
  "is:delete-issue": {
    "type": "void",
    "href": "...",
    "method": "DELETE",
    "title": "Delete issue"
  }
}
```

##### Action type `json`
JSON actions are for sending structured JSON data when performing an action. The HTTP request MUST be of type "application/json".

The `template` property may contain any JSON value and the client is expected to use this template as the default value for its action request. **TODO**: MUST the client use the template or SHOULD the client use it? See GitHub issue #1.

The `schemaUrl`property is a reference to a schema which the client may use to validate the JSON data before sending or create default data from when no `template` property is present. The schema may be [JSON-Schema](http://json-schema.org/) but can be any kind of schema language for JSON objects and clients should check the content type of the schema resource before blindly assuming it is JSON schema.

**Example usage of `json` action**

```json
"@actions": {
  // JSON action with schema reference
  "is:project-create": {
    "type": "json",
    "href": "...",
    "title": "Create new project",
    "schemaUrl": "..."
  },
  // JSON action with default template
  "is:update-project": {
    "type": "json",
    "href": "...",
    "title": "Update project details",
    "template": {
        "Code": "SHOP",
        "Title": "Webshop",
        "Description": "All issues related to the webshop."
    }
  }
}
```

##### Action type `json-files`
JSON+Files actions are for sending binary files together with structured JSON data when performing an action. The HTTP request MUST be of type [`multipart/form-data`](http://www.ietf.org/rfc/rfc2388.txt).

The media type `multipart/form-data` is an efficient format for combining multiple files into one single message. It consists of parts where each part has a name and associate content type.

With JSON+Files clients are expected to send a JSON document as a part of the message which name is defined by the `jsonFile` property. Additional files must be named according to the `name`property in the `files` array.

The `schemaUrl` and `template` properties are interpreted in the same way as for plain "json" actions and applies to the JSON part of the multipart message.

**Example usage of `json+files` action**

This example instructs the client to send the JSON document in the part `args` and one additional file in the part `attachment`.

```json
"@actions": {
  "is:add-issue": {
    "type": "json+files",
    "href": "...",
    "title": "Add new issue to project with optional attachment",
    "schemaUrl": "...",
    "jsonFile": "args",
    "files": [
        {
            "name": "attachment",
            "description": "Attachment for issue"
        }
    ]
  }
}
```

##### Action type `any`

The action type `any` is a "catch all" for sending any kind of data in an action.

**Example usage of `any` action**

```json
"@actions": {
  "is:update-attachment": {
    "type": "any",
    "href": "...",
    "method": "PUT",
    "title": "Update content of attachment."
  }
}
```

#### Properties for `@actions`

##### `<name>` (property name)
Property names define the action name.

##### `type`
This property is REQUIRED and MUST be a string value of either `void`, `json`, `json+files` or `any`.

##### `href`
This property is REQUIRED and MUST be a string value representing a valid URI. It defines the target of the action.

##### `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the action.

Default method is POST if no `method`is specified.

##### `title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short descriptive title for the action.

This property can safely be removed in minimized representations.

##### `description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains some descriptive text for the action.

This property can safely be removed in minimized representations.

##### `schemaUrl` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a valid URL. The URL must reference a schema for JSON objects.

##### `template` (optional)
This property is OPTIONAL. If present it can be any JSON value.

##### `jsonFile` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the name of the part containing JSON data when using JSON+Files actions.

##### `files` (optional)
This property is OPTIONAL. If present it MUST be an array of file definition objects as described below.

#### Files

Each entry in the `files` property defines a file to be send in the multipart message when using JSON+Files actions.

##### `files[].name`
This property is REQUIRED and MUST be a string. It defines the name of the part for sending the file.

##### `files[].description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text for the file.

This property can safely be removed in minimized representations.


### `@error`

The `@error` property is OPTIONAL. If present it MUST be an object. It can only be present in the root object.

The error object contains information on error conditions that has occured during the latest operation.

The error object can be extended with additional application specific properties and thus the standard Mason error properties must be prefixed with '@'.

**Example usage of `@error`**

```json
"@error": {
  "@id": "b2613385-a3b2-47b7-b336-a85ac405bc66",
  "@message": "There was a problem with one or more input values.",
  "@code": "INVALIDINPUT",
  "@messages": [
    "title should not be empty or consist only of white-space characters. Parameternavn: title"
  ]
}
```

#### Properties for `@error`

##### `@message`
This property is REQUIRED and MUST be a string value. It should be a human readable error message directed at the end users.

##### `@id` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a unique identifier for later reference to the situation that resulted in a error condition (for instance when looking up a log entry).

##### `@code` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a code describing the error condition in general. 

##### `@messages` (optional)
This property is OPTIONAL. If present it MUST be an array of strings. It should contain an array of additional human readable error messages directed at the end user.

##### `@details` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain an extensive human readable message to the client developer.

##### `@httpStatusCode` (optional)
This property is OPTIONAL. If present it MUST be a an integer value. It should contain the HTTP status code from the latest response.

##### `@links` (optional)
This property is OPTIONAL. If present it MUST be an object adhering to the same rules as the top `@links` object. It should contain links to resources that are relevant for the error condition. It can be links for both end users as well as client developers. A generic client won't know the difference but specific implementations can decide to use certain link relations for either of the audiences.

##### `@time` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a date in the format defined by [RFC 3339](http://tools.ietf.org/html/rfc3339). Example: "1985-04-12T23:20:50.52Z". It should contain a timestamp of when the error condition occured.

