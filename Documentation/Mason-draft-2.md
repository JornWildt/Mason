** THIS IS WORK IN PROGRESS **

# Introduction

> Note: The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](http://tools.ietf.org/html/rfc2119).

Mason is a JSON based format for adding hypermedia elements, standardized error handling and additional meta data to classic JSON representations. It is a generic format and imposes very little restrictions on the data it is integrated with.

A Mason document is constructed by taking a "classic" JSON object and then merging hypermedia elements and other Mason features into it. Mason properties are prefixed with `@` to avoid collisions with existing property names.

Here is a simple example to introduce the format. Suppose we have an existing payload representing a single issue from an issue tracker. It could look as shown below without any Mason specific elements:

```json
{
  "ID": 1,
  "Title": "Program crashes when pressing ctrl-p",
  "Description": "I pressed ctrl-p and, boom, it crashed.",
  "Severity": 5,
  "Attachments": [
    {
      "Id": 15,
      "Title": "Error report"
    }
  ]
}
```

Now we can add a few links to the issue: a link to the issue itself (a "self" link), a link to the issue's containing project (an "up" link) and a link to the attachment (another "self" link in a different context).

Links (and other hypermedia elements) are added as object properties in a special "@navigation" element:

```json
{
  "ID": 1,
  "Title": "Program crashes when pressing ctrl-p",
  "Description": "I pressed ctrl-p and, boom, it crashed.",
  "Severity": 5,
  "Attachments": [
    {
      "Id": 1,
      "Title": "Error report",
      "@navigation": {
        "self": {
          "href": "http://issue-tracker.org/attachments/1"
        }
      }
    }
  ],
  "@navigation": {
    "self": {
      "href": "http://issue-tracker.org/issues/1"
    },
    "up": {
      "href": "http://issue-tracker.org/projects/1",
      "title": "Containing project"
    },
  }
}
```

# Syntax

Mason is based on JSON and follows as such all the syntax rules for valid JSON documents. Mason reserves the character '@' as a prefix for Mason property names.

The prefix character is not used for all Mason properties, only for those that co-exists with other properties from the underlying resource data - for instance '@meta' and '@actions'. Other property names like "template" and "parameters" are used only in contexts where it is not allowed to mix data so these properties do not use the '@' prefix.


# Curies

The word "Curie" is an abbreviation for "Compact URI" and is a way to define short scoped names that map to URIs. Mason uses namespace declarations to declare prefixes for use in Curies. A Curie is expanded to a URI by replacing the namespace prefix with the corresponding name declared in the `@namespaces` object.

Curies are only expanded in navigation identifiers - not in target URLs of links and other elements.

See [CURIE Syntax 1.0](http://www.w3.org/TR/2009/CR-curie-20090116/) for further information.


# Expected client processing rules

When a client requests a Mason document it may be looking for some specific data, trying to discover a specific link or invoking some kind of action on the server. To perform any of these operations the client should process the Mason document in a standardized way as described here:

  1. Register all namespace declarations. These are key/value pairs that map namespace names into URI prefixes.
  
  2. Iterate recursively through all `@navigation` elements and expand navigational element names (curies) using the namespace declarations.
  
  3. Read whatever JSON data the client is looking for.
  
  4. If the client tries to invoke any navigational element it SHOULD be prepared to handle any kind of navigational element - it SHOULD NOT assume a fixed type of navigational element. This allows the server to use the type of navigational element that fits best at any given time - without breaking clients.
  
## Processing of navigational elements

A client trying to invoke a navigational element should follow the instructions described below.

  1. Prepare a JSON object with the data expected to be necessary to invoke the navigational element. If there is no data available then use an empty JSON object (this could for instance be the case when the client expects to follow a link). This is the *argument object*.
  
  2. If the navigational element is a link then simply follow it.
  
  3. If the navigational element is a URL template then expand the template using the argument object as a dictionary containing variables for the expansion.
  
  4. If the navigational element is a void action then ignore the argument object and invoke the action.
  
  5. If the navigational element is a JSON or JSON+Files action then use the argument object as input values to the action and invoke it.
  
  6. If the navigational element is a generic action then the behavior is undefined and must depend on some prior agreement with the server.


# Minimized responses

Some of the Mason elements are only needed for client developers exploring the API - for instance the @meta element and "title" property of links. These elements can be removed by the server at runtime to reduce the size of the payload and thus save some bandwith.

The recommended way of instructing the server to return a minimal response is to pass the value "representation=minimal" in the ["Prefer" header](http://tools.ietf.org/html/draft-snell-http-prefer-18).


# Reserved property names

This is the complete list of reserved property names and their semantics. Mason may add new properties prefixed with `@` in future versions and clients must be prepared for this. Unknown Mason properties should be ignored by clients.


## `@meta`

The `@meta` property is OPTIONAL. If present it MUST be an object value. It can only be present in the root object.

The meta object contains information targeted at client developers exploring or debugging the API. The intention of the meta information is to document and highlight details about the API but it is not restricted to this use.

The meta object can be extended with additional application specific properties and thus the standard Mason meta properties must be prefixed with '@'.

**Example usage of `@meta`**

```json
"@meta": {
    "@title": "Issue",
    "@description": "This resource represents a single issue with its data and related actions.",
    "@navigation": {
        "terms-of-service": {
            "href": "...",
            "title": "Terms of service"
        }
    }
}
```

The meta object can safely be removed in minimized representations. Clients can safely ignore it if present.

### Properties for `@meta`

#### `@title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a descriptive title.

#### `@description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text.

#### `@navigation` (optional)
This property is OPTIONAL. If present it MUST be an object with links. It defines links to other resources that are relevant for client developers - for instance API documentation or terms of service. This property may also contain other navigational elements than links if necessary (but it is not recommended).


## `@namespaces`

The `@namespaces` property is OPTIONAL. If present it MUST be an object value. It can only occur in the root object (Mason does not support nested namespace declarations).

The namespaces object contains a set of namespace objects indexed by their namespace prefix. Each namespace object defines the namespace URI using the property `name`.

Namespaces are used to expand curies in navigational names.

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

### Properties for `@namespaces`

#### `<prefix>` (property name)
Property names define namespace prefix.

#### `name`
This property is REQUIRED and MUST be a string value. It contains the URI for the namespace.


## `@navigation`

The `@navigation` property is OPTIONAL. If present it MUST be an object value. It is not restricted to the root object and may occur in any nested data object.

The `@navigation` property represents different ways of navigating and modifying resources. The simplest possible navigation element is a link which represents a named link from the containing resource to another target resource. Other navigation elements are link templates, "void" actions, "json" actions, "json+files" actions and "generic" actions. The type of navigational element is indicated with the property `type` included in each navigational element.

The set of navigational elements in the `@navigation` object is indexed by their respective identifiers. These identifiers are sometimes called "relationship types" when refering to a link, but in the following sections we will simply refer to the identifiers as "name".

The name of a navigational element can either be a simple predefined token from the [IANA relationship registry](http://www.iana.org/assignments/link-relations/link-relations.xhtml), a curie or a complete URI. The use of URIs (and curies) as a namespace mechanism makes it easy to declare names without colliding with similar names from other systems.

Here a few examples of different ways to name a navigational element:

**Standard IANA *self* link**

```json
"@navigation": {
  "self": {
    "href": "..."
  }
}
```

**Non standard link identified by a curie**

This is equivalent to a link name "http://issue-tracker-reltypes.org/rels#contact".

```json
"@namespaces" :
{
  "is": { 
    "name": "http://issue-tracker-reltypes.org/rels#" 
  }
},
"@navigation": {
  "is:contact": {
    "href": "...",
    "title": "Complete contact information."
  }
}
```

**Non standard link identified by a complete URI**

```json
"@navigation": {
  "http://issue-tracker-reltypes.org/rels#logo": {
    "href": "...",
    "title": "Image of the logo for this instance of issue tracker.",
    "content_type": "image/png"
  }
}
```

**Non standard link with alternate URLs for other types**

```json
"@navigation": {
  "is:contact": {
    "href": "...",
    "title": "Contact information as vCard",
    "content_type": "text/vcard",
    "alt": [
      {
        "href": "...",
        "title": "Contact information as jCard",
        "content_type": "application/vcard+json"
      }
    ]
  }
}
```

### Common properties for `@navigation`

These are the properties that are common for all types of navigational elements (links, link templates and the various types of actions).

Navigational elements are not extendable and thus their property names need not be prefixed with '@'.

#### `<name>` (property name)
Property names define the navigational element name. In this way the `@navigation` object is indexed by the navgational element names.

#### `href`
This property is REQUIRED and MUST be a string value representing a valid URI. It contains the target URI of the navigational element - or a URL template to be completed thorugh variable expansion.

The `href` URI SHOULD be an absolute URL but clients should be prepared to handle relative URLs. At the time of writing there is no rules for how to resolve relative URLs so it will have to depend on an agreement between the client and server.

#### `type`
This property is OPTIONAL. If present it MUST be a string value representing the type of navigational element. The possible values for `type`are:

  * `link`: a link.
  * `link-template`: a link template.
  * `void`: an action with no payload.
  * `json`: a JSON action.
  * `json+files`: a JSON action with file attachments.
  * `any`: a generic action.
  
If `type` is not present it is assumed to be `link`.

#### `title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short descriptive title.

This property can safely be removed in minimized representations.

#### `description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a long descriptive text.

This property can safely be removed in minimized representations.

#### `content_type` (optional)
This property is OPTIONAL. If present it MUST be a string value. It specifies the expected content type of the target resource.

#### `alt` (optional)
This property is OPTIONAL. If present it MUST be an array of navigational elements each of which represents alternatives to the primary navigational element (see next section).

Example:

```json
"@navigation": {
  "author": {
    "title": "Link to contact details for author (represented in Mason).",
    "href": "...",
    "content_type": "application/vnd.mason+json"
    "alt":
    [
      {
        "title": "Link to contact details for author (represented as a vCard).",
        "href": "...",
        "content_type": "text/vcard"
      }
    ]
  }
}
```


### Alternative navigational elements

All navigational elements may have one primary element and many alternative variations (or no variations). The alternative elements are stored in the `alt` property of the primary element. The `alt` property MUST be an array of navigational elements which are supposed to be equivalent to the primary navigational element but differ on for instance expected content type of the response or payload encoding. This makes it simple for clients to access the most used navigational element and if they are aware of possible alternatives then they can search the alternative elements for better ways of interacting with the server.

Alternative elements are mostly known to represent links to different representations of the same resource.

Here is an example of a link to the contact details for the author of a certain piece of data. The primary version is expected to return a Mason response whereas the alternative version returns a vCard representation of the contact details:

```json
"@navigation": {
  "author": {
    "title": "Link to contact details for author.",
    "href": "...",
    "content_type": "application/vnd.mason+json"
    "alt":
    [
      {
        "title": "Link to contact details for author (as vCard).",
        "href": "...",
        "content_type": "text/vcard"
      }
    ]
  }
}
```

Alternative elements should differ from the primary element in either "type", "content_type", or "method".


### Links

Links represents a relationship between one resource and another as described in [RFC 5988 Web Linking](http://tools.ietf.org/search/rfc5988). The relationship between the two resources is assigned a name (the relationship type) which is used by the client to locate the link in the set of navigation elements.

Example:

```json
"@navigation": {
  "self": {
    "title": "Links to this resource",
    "description": "Follow this link to get the representation of this resource",
    "href": "...",
    "content_type": "application/vnd.mason+json"
  }
}
```

Links does not have any properties in addition to the common navigational properties.


### Link templates

A link template represents an set of links with different URLs available through variable expansion as described in [RFC 6570 - URI Template](https://tools.ietf.org/html/rfc6570).

The URL template itself is stored in the `href` property just link links does - except that the `href` value will be a URL template instead of a complete URL.

The simplest templates consists of placeholdes for variable values. The placeholders are identified by curly braces as for instance "{severity}".

**Example usage of a link template**

```json
"@navigation": {
  "is:issue-query": {
    "type": "link-template",
    "href": "http://.../issues-query?text={text}&severity={severity}&project={pid}",
    "title": "Search for issues",
    "description": "This is a simple search that does not check attachments.",
    "parameters": [
      {
        "name": "text",
        "title": "Query text",
        "description": "Substring search for text in title and description"
      },
      {
        "name": "severity",
        "title": "Severity",
        "description": "Issue severity (exact value, 1..5)"
      },
      {
        "name": "pid",
        "title": "Project ID"
      }
    ]
  }
}
```


#### Properties for link templates

Link templates share all the common navigational element properties.

##### `href`
This property is REQUIRED and MUST be a string value representing a valid URL template according to [RFC 6570](http://tools.ietf.org/html/rfc6570).

##### `parameters` (optional)
This property is OPTIONAL. If present it MUST be an array of parameter definition objects as described below.

It can be removed in minimized representations assuming the clients are hard coded with the knowledge if they request a minimized response.

#### Template parameters

Each entry in the `parameters` property defines a parameter variable for the template.

##### `parameters[].name`
This property is REQUIRED and MUST be a string. It defines the name of the parameter.

##### `parameters[].title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short title for the parameter.

This property can safely be removed in minimized representations.

##### `parameters[].description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text for the parameter.

This property can safely be removed in minimized representations.


### Void actions

Void actions are for use with requests that carries no payload - for instance when issuing an HTTP DELETE operation.

**Example usage of `void` action**

```json
"@navigation": {
  "is:delete-issue": {
    "type": "void",
    "href": "...",
    "method": "DELETE",
    "title": "Delete issue"
  }
}
```

#### Properties for void actions

Void actions share all the common navigational element properties.

##### `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the action.

Default method is POST if no `method` is specified.


### JSON Actions

JSON actions are for sending structured JSON data when performing an action. The HTTP request MUST be of type "application/json".

The `schemaUrl` property is a reference to a schema which the client may use to validate the JSON data. The schema may also be used to create default data from when no `template` property is present. The schema may be [JSON-Schema](http://json-schema.org/) but can be any kind of schema language for JSON and clients should check the content type of the schema resource before blindly assuming it is JSON schema.

The server may supply a `template` property which can contain any kind of JSON value. The client is expected to use this as a building block when creating a request. To do so the client first reads the template value and then modifies it to reflect the changes the clients want to happen. Any unrecognized data in the template object MUST be left unmodified and sendt back in the request.

The purpose of the template value is:

  1. Make it possible to supply useful default data to display before modifying.
  2. Make it possible to supply default values for data that older clients are unaware of.
  3. Make it possible to supply "hidden" values for authorization, logging and other sorts of internal book keeping.

**Example usage of `json` action**

Simple JSON action:

```json
"@navigation": {
  "is:project-create": {
    "type": "json",
    "href": "...",
    "title": "Create new project"
  }
}
```

Complex JSON action with schema reference and template containing default values for the action:

```json
"@navigation": {
  "is:update-project": {
    "type": "json",
    "href": "...",
    "title": "Update project details",
    "schemaUrl": "...",
    "template": {
        "Code": "SHOP",
        "Title": "Webshop",
        "Description": "All issues related to the webshop."
    }
  }
}
```

#### Properties for JSON actions

JSON actions share all the common navigational element properties.

##### `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the action.

Default method is POST if no `method` is specified.

##### `schemaUrl` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a valid URL. The URL must reference a schema for JSON objects.

##### `template` (optional)
This property is OPTIONAL. If present it can be any JSON value.



### JSON+Files action with binary file data

JSON+Files actions are for sending binary files together with structured JSON data when performing an action. The HTTP request MUST be of type [`multipart/form-data`](http://www.ietf.org/rfc/rfc2388.txt).

The media type `multipart/form-data` is an efficient format for combining multiple files into one single message. It consists of parts where each part has a name and associate content type.

With JSON+Files clients are expected to send a JSON document as a part of the message. The name of this part is defined by the `jsonFile` property. Additional files must be named according to the `name`property in the `files` array.

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

The resulting request could look like this:

```text
POST /projects/1/issues HTTP/1.1
Content-Type: multipart/form-data; boundary=04149776-d03d-4eac-941c-dafffececb28
Content-Length: 33816


--04149776-d03d-4eac-941c-dafffececb28
Content-Disposition: form-data; name="attachment"; filename="screendump.png"
... image data not included ...

--04149776-d03d-4eac-941c-dafffececb28
Content-Disposition: form-data; name="args"; filename="args"
Content-Type: application/json

{
  "Title": "...",
  "Description": "..."
}

```


#### Properties for JSON+Files actions

JSON+Files actions share all the common navigational element properties.

##### `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the action.

Default method is POST if no `method` is specified.

##### `schemaUrl` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a valid URL. The URL must reference a schema for JSON objects.

##### `template` (optional)
This property is OPTIONAL. If present it can be any JSON value.

##### `jsonFile` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the name of the part containing JSON data when using JSON+Files actions.

If no `jsonFile` property is specified then the client may choose to send JSON data anyway in a part with a name chosen by the client. The server's reaction to this is unspecified.

##### `files` (optional)
This property is OPTIONAL. If present it MUST be an array of file definition objects as described below.

#### Files

Each entry in the `files` property defines a file to be send in the multipart message when using JSON+Files actions.

##### `files[].name`
This property is REQUIRED and MUST be a string. It defines the name of the part for sending the file.

##### `files[].title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short title for the file.

##### `files[].description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains descriptive text for the file.

This property can safely be removed in minimized representations.


### Generic actions with any kind of payload

The action type `any` is a catch all for sending any kind of data in an action.

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

#### Properties for generic actions with any kind of payload

Generic actions share all the common navigational element properties.

##### `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the action.

Default method is POST if no `method` is specified.


## `@error`

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

### Properties for `@error`

#### `@message`
This property is REQUIRED and MUST be a string value. It should be a human readable error message directed at the end users.

#### `@id` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a unique identifier for later reference to the situation that resulted in a error condition (for instance when looking up a log entry).

#### `@code` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a code describing the error condition in general. 

#### `@messages` (optional)
This property is OPTIONAL. If present it MUST be an array of strings. It should contain an array of additional human readable error messages directed at the end user.

#### `@details` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain an extensive human readable message to the client developer.

#### `@httpStatusCode` (optional)
This property is OPTIONAL. If present it MUST be a an integer value. It should contain the HTTP status code from the latest response.

#### `@links` (optional)
This property is OPTIONAL. If present it MUST be an object adhering to the same rules as the top `@links` object. It should contain links to resources that are relevant for the error condition. It can be links for both end users as well as client developers. A generic client won't know the difference but specific implementations can decide to use certain link relations for either of the audiences.

#### `@time` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a date in the format defined by [RFC 3339](http://tools.ietf.org/html/rfc3339). Example: "1985-04-12T23:20:50.52Z". It should contain a timestamp of when the error condition occured.

