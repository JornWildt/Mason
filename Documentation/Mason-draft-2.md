# Mason format specification Draft 2

This document replaces Draft 1 of the Mason format specification.

# Introduction

> Note: The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](http://tools.ietf.org/html/rfc2119).

Mason is a JSON based format for adding hypermedia elements, standardized error handling and additional meta data to classic JSON representations. It is a generic format and imposes very little restrictions on the data it is integrated with.

A Mason document is constructed by taking a "classic" JSON object and then merging hypermedia elements and other Mason features into it. Mason properties are prefixed with `@` to avoid collisions with existing property names.

A Mason document consists of the following types of elements:

  * Core bussiness data (such as the title of a book, number of new messages, identifiers and so on).
  
  * Meta data about the resource targeted at the client developer.
  
  * Various kinds of hypermedia elements such as links, link templates and actions. These are generally referred to as *hypermedia control elements*.
  
  * Namespace declarations for expansion of compact URIs (Curies) in control elements.
  
  * Error details.

Here is a simple example to introduce the format: suppose we have an existing payload representing a single issue from an issue tracker. It could look as shown below without any Mason specific elements:

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

Links (and other hypermedia elements) are added as object properties in a special "@controls" element:

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
      "@controls": {
        "self": {
          "title": "Attachment details",
          "href": "http://issue-tracker.org/attachments/1"
        }
      }
    }
  ],
  "@controls": {
    "self": {
      "href": "http://issue-tracker.org/issues/1"
    },
    "up": {
      "title": "Containing project",
      "href": "http://issue-tracker.org/projects/1"
    },
  }
}
```

We can also add a few controls that represents different ways of modifying the issue. Often we call such controls that modifies server state for *actions*.

Below we have added the cntrols "is:add-issue" for adding a new issue and "is:delete-issue" for deleting the issue (the prefix "is:" is for compact URI expansion - a shorthand notation for URIs to be explained later):

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
      "@controls": {
        "self": {
          "href": "http://issue-tracker.org/attachments/1"
        }
      }
    }
  ],
  "@controls": {
    "self": {
      "href": "http://issue-tracker.org/issues/1"
    },
    "up": {
      "href": "http://issue-tracker.org/projects/1",
      "title": "Containing project"
    },
    "is:add-issue": {
      "type": "json",
      "href": "http://issue-tracker.org/issues",
      "schemaUrl": "http://..."
    },
    "is:delete-issue": {
      "type": "void",
      "href": "http://issue-tracker.org/issues/1",
      "method": "DELETE"
    }
  }
}
```


# Syntax

Mason is based on JSON and follows as such all the syntax rules for valid JSON documents. Mason reserves the character '@' as a prefix for Mason property names.

The prefix character is not used for all Mason properties, only for those that co-exists with other properties from the underlying resource data - for instance '@meta' and '@actions'. Other property names like "href" and "title" are used only in contexts where it is not allowed to mix data so these properties do not use the '@' prefix.


# Curies

The word "Curie" is an abbreviation for "Compact URI" and is a way to define short scoped names that map to URIs. Mason uses namespace declarations to declare prefixes for use in Curies. A Curie is expanded to a URI by replacing the namespace prefix with the corresponding name declared in the `@namespaces` object.

Curies are only expanded in control element identifiers - not in target URIs of links and other elements.

This means the following two control examples are considered equivalent:

```json
{
  "@controls": {
    "http://soabits.dk/mason/issue-tracker/reltypes.html#add-issue": {
      "type": "json",
      "href": "http://issue-tracker.org/issues"
    }
  }
}
```

```json
{
  "@namespaces": {
    "is": {
      "name": "http://soabits.dk/mason/issue-tracker/reltypes.html#"
    }
  },  
  "@controls": {
    "is:add-issue": {
      "type": "json",
      "href": "http://issue-tracker.org/issues"
    }
  }
}
```

The purpose of curies is to improve readability of the raw JSON data. It does save a few bytes in the Mason document but that is not the primary focus as compression will do a much better job.

See [CURIE Syntax 1.0](http://www.w3.org/TR/2009/CR-curie-20090116/) for further information.


# Client processing rules

When a client requests a Mason document it may be looking for some specific data, trying to discover a specific link or invoking some kind of action on the server. To perform any of these operations the client should process the Mason document in a standardized way as described here:

  1. Register all namespace declarations. These are key/value pairs that map namespace names into URI prefixes.
  
  2. Iterate recursively through all JSON objects and locate @controls elements. For each of the controls expand control element names (curies) using the namespace declarations.
  
  3. Read whatever JSON data the client is looking for in the document.
  
  4. If the client tries to invoke a control it SHOULD be prepared to handle any kind of control type - it SHOULD NOT assume a fixed type of control. This allows the server to use the type of controls that fits best at any given time - without breaking clients.
  
  
## Invoking control elements

A client trying to invoke a control element should follow the instructions described below. By doing so the client will be able to handle various changes to the control without breaking. It will for instance be possible to change from a GET URI template to a JSON POST request without modifications to the client.

  1. Prepare a JSON object with the data expected to be necessary to invoke the control. If there is no data available then use an empty JSON object (this could for instance be the case when the client expects to follow a link). This is the *arguments* object. The structure of the arguments object can either be hard coded into the client, discovered through a schema or by some other means.
  
  1. If the control has a templated `href` URI (as indicated by the `isHrefTemplate` property) then do variable expansion on the template using the arguments object as input.

  1. Resolve relative `href` URLs as per [RFC 1808](https://tools.ietf.org/html/rfc1808). Mason does not have a feature for specifing the base URL within a Mason document so section 3.1 "Base URL within Document Content" does not apply.
  
  1. If `template` is set then merge the arguments object into the template object and replace *arguments* with the result. This will ensure that unknown properties in the template object is kept unchanged and sent back to the server.
  
  1. If `encoding` is set to `json` then serialize the arguments object into the request body as a JSON string.
  
  1. If `encoding` is set to `json+files` then create one multipart entry for each attached file. Then serialize the arguments object into another multipart entry and name it according to the `jsonFile` property. The set of files can either be hard coded into the client, discovered through the `files` definition or by some other means.
  
  1. If encoding is set to `raw` then the request body format must be coordinated with the server in some other way - for instance through written documentation. The `accept` property may indicate what kind of media types the server accepts.


# Minimized responses

Some of the Mason elements are only needed for client developers exploring the API - for instance the @meta element and "title" property of links. These elements can be removed by the server at runtime to reduce the size of the payload and thus save some bandwith.

The recommended way of instructing the server to return a minimal response is to pass the value "representation=minimal" in the ["Prefer" header](http://tools.ietf.org/html/draft-snell-http-prefer-18).


# Reserved property names

This is the complete list of reserved property names and their semantics. Mason may add new properties prefixed with `@` in future versions and clients must be prepared for this. Unknown Mason properties should be ignored by clients.


## Property name `@meta`

The `@meta` property is OPTIONAL. If present it MUST be an object value. It can only be present in the root object.

The meta object contains information targeted at client developers exploring or debugging the API. The intention of the meta information is to document and highlight details about the API but it is not restricted to this use.

The meta object can be extended with additional application specific properties and thus the standard Mason meta properties must be prefixed with '@'.

**Example usage of `@meta`**

```json
"@meta": {
    "@title": "Issue",
    "@description": "This resource represents a single issue with its data and related actions.",
    "@controls": {
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

#### `@controls` (optional)
This property is OPTIONAL. If present it MUST be an object adhering to the same rules as the normal `@controls` object. It can for instance contain links to other resources that are relevant for client developers such as API documentation or terms of service. This property may also contain other control elements than links.


## Property name `@namespaces`

The `@namespaces` property is OPTIONAL. If present it MUST be an object value. It can only occur in the root object (Mason does not support nested namespace declarations).

The namespaces object contains a set of namespace objects indexed by their namespace prefix. Each namespace object defines the namespace URI using the property `name`.

Namespaces are used to expand curies in control element names.

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


## Property name `@controls`

The `@controls` property is OPTIONAL. If present it MUST be an object value. It is not restricted to the root object and may occur in any nested data object.

The `@controls` property is an object that represents various hypermedia elements for controlling the application. Each property in the object represents a single hypermedia control.

Each control is represented by a set of properties that defines its behavior. By combining these properties Mason can represent links, link templates and more complex hypermedia controls that, among other things, allows for uploading of files and modifying existing resources.

### Naming of controls

The set of controls in the `@controls` object is indexed by their respective identifiers. These identifiers are sometimes called "relationship types" when refering to a link, but in the following sections we will simply refer to the identifiers as "names".

The name of a control can either be a simple predefined token from the [IANA relationship registry](http://www.iana.org/assignments/link-relations/link-relations.xhtml), a curie or a complete URI. The use of URIs (and curies) as a namespace mechanism makes it easy to declare names without colliding with similar names from other systems.

Here are a few examples of different ways to name a control:

**Standard IANA *self* link**

```json
"@controls": {
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
"@controls": {
  "is:contact": {
    "href": "...",
    "title": "Complete contact information."
  }
}
```

**Non standard link identified by a complete URI**

```json
"@controls": {
  "http://issue-tracker-reltypes.org/rels#logo": {
    "href": "...",
    "title": "Image of the logo for this instance of issue tracker.",
    "output": ["image/png"]
  }
}
```


### Alternative control elements

All controls may have one primary element and many alternative variations (or no variations). The alternative elements are stored in the `alt` property of the primary element. The `alt` property MUST be an array of control elements which are supposed to be equivalent to the primary control element but differ on for instance expected content type of the response or payload encoding. This makes it simple for clients to access the most used control element and if they are aware of possible alternatives then they can search the alternative elements for better ways of interacting with the server.

Alternative elements are mostly known to represent links to different representations of the same resource.

Here is an example of a link to the contact details for the author of a certain piece of data. The primary version is expected to return a Mason response whereas the alternative version returns a vCard representation of the contact details:

```json
"@controls": {
  "author": {
    "title": "Link to contact details for author.",
    "href": "...",
    "output": ["application/vnd.mason+json"],
    "alt":
    [
      {
        "title": "Link to contact details for author (as vCard).",
        "href": "...",
        "output": ["text/vcard"]
      }
    ]
  }
}
```


### Control properties

A single hypermedia control element can be represented by the following properties:

* **href** [string, required]: Hypermedia reference - a URI or URI template.

* **isHrefTemplate** [bool, optional]: Boolean indicating whether "href" is a URI template or concrete URI (default values is false).

* **title** [string, optional]: Title of the control.

* **description** [string, optional]: Description of the control.

* **method** [string, optional]: HTTP method to use (default value is "GET").

* **encoding** [string, optional]: Required encoding of data in request body. Possible values are "none", "json", "json+files" and "raw" (default value is "none").

* **schema** [object, optional]: Embedded schema definition of request body and href template parameters.

* **schemaUrl** [string, optional]: URL to referenced schema definition of request body and href template parameters.

* **template** [object, optional]: Request template data.

* **accept** [array of string, optional]: List of accepted media types.

* **output** [array of string, optional]: List of possible returned media types.

* **files** [optional, array of objects]: List of parts definition for multipart requests.

* **alt** [array, optional]: list of alternative equivalent controls.

Control elements are not extendable and thus their property names need not be prefixed with '@'.


#### Control property `<name>` (property name)
The property name (as used in the `@controls` object) defines the control name. In this way the `@controls` object is indexed by the control names.


#### Control property `href`
This property is REQUIRED and MUST be a string value representing a valid URI or URI template. It contains the target URI of the control or a URI template to be completed thorugh variable expansion.

The `href` URI SHOULD be an absolute URI (or URI template) but clients should be prepared to handle relative URIs. At the time of writing there is no rules for how to resolve relative URIs so it will have to depend on an agreement between the client and server.

If `isHrefTemplate` is true then `href` must be interpreted as a URI template according to [RFC 6570 - URI Template](https://tools.ietf.org/html/rfc6570). The template parameters may be described by a schema definition in the `schema` property or through a referenced schema via the `schemaUrl` property.


#### Control property `isHrefTemplate`
This property is OPTIONAL. If present it MUST be a boolean value indicating whether the `href` value is a URI or a URI template.


#### Control property `title` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a short descriptive title.

This property can safely be removed in minimized representations.


#### Control property `description` (optional)
This property is OPTIONAL. If present it MUST be a string value. It contains a descriptive text.

This property can safely be removed in minimized representations.


#### Control property `method` (optional)
This property is OPTIONAL. If present it MUST be a string value. It defines the HTTP method to use in the request.

Default method is GET if no `method` is specified AND `encoding` is "none" (the default value). Otherwise the default method is POST if no `method` is specified AND `encoding` is anything else than "none". This means that controls without HTTP body default to GET whereas other controls default to POST.


#### Control property `encoding`
This property is OPTIONAL. If present it MUST be a string value indicating the required encoding of the request body.

Encoding does not indicate a general content type specification. Instead it indicates which of a small set of rules to use for encoding the payload.

The possible values for `encoding` are:

  * `none`: No request body expected.
  * `json`: Request body must be encoded as application/json.
  * `json+files`: Request body must be encoded as multipart/form-data with JSON data represented in one of the parts. In this case the `jsonFile` property identifies the name of the part to embed the JSON data in.
  * `raw`: No special encoding is required for the request body. In this case the `accept` property may be used to indicate expected content type of the request.
  
If `encoding` is not present it is assumed to be `none`.


#### Control property `schema` (optional)
This property is OPTIONAL. If present it MUST be an object representing a schema definition describing the possible structure of the request body.

If `encoding` is either "json" or "json+files" then the schema should describe the JSON data.

The default schema language is [JSON schema](http://json-schema.org/).

Example:

```
{
  "@namespaces": {
    "is": {
      "name": "http://elfisk.dk/mason/issue-tracker/reltypes.html#"
    }
  },
  "@controls": {
    "is:project-create": {
      "title": "Create project",
      "description": "Add new project to issue tracker.",
      "encoding": "json",
      "href": "...",
      "method": "POST",
      "schema": {
        "type": "object",
        "properties": {
          "Code": {
            "type": "string"
          },
          "Title": {
            "type": "string"
          },
          "Description": {
            "type": "string"
          }
        }
      }
    }
  }
}```


#### Control property `schemaUrl` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a valid URL. The URL must reference a schema file describing the possible structure of the request body.

If `encoding` is either "json" or "json+files" then the schema should describe the JSON data.

The default schema language is [JSON schema](http://json-schema.org/).


#### Control property `template` (optional)
This property is OPTIONAL. If present it can be any JSON value representing the default value of the request.

Clients should read the `template` value (if present) and merge their calculated JSON request data into it before serializing the result into the request body.

The purpose of the template value is:

  1. To supply useful default data to display before modifying.
  2. To supply default values for data that older clients are unaware of.
  3. To supply "hidden" values for authorization, logging, "ETag" like values and other sorts of data for internal book keeping.

Any unrecognized data in the template object MUST be left unmodified and sendt back in the request.

**Example usage of template**

This example contains a template with values for "Code", "Title", "Description" and some sort of authentication token known to the server only.

```json
"@controls": {
  "is:update-project": {
    "type": "json",
    "href": "...",
    "title": "Update project details",
    "schemaUrl": "...",
    "template": {
        "Code": "SHOP",
        "Title": "Webshop",
        "Description": "All issues related to the webshop.",
        "AuthToken": "jh987yfm16"
    }
  }
}
```


#### Control property `accept` (optional)
This property is OPTIONAL. If present it must be an array of strings representing the request content types accepted by the server.


#### Control property `output` (optional)
This property is OPTIONAL. If present it must be an array of strings representing the content types a client can expect to receive from the server.


#### Control property `alt` (optional)
This property is OPTIONAL. If present it MUST be an array of control elements each of which represents alternatives to the primary control element (see previous section).

Example:

```json
"@controls": {
  "author": {
    "title": "Link to contact details for author (represented in Mason).",
    "href": "...",
    "output": ["application/vnd.mason+json"]
    "alt":
    [
      {
        "title": "Link to contact details for author (represented as a vCard).",
        "href": "...",
        "output": ["text/vcard"]
      }
    ]
  }
}
```


#### Control property `files`

This property is OPTIONAL. If present it MUST be an array of objects each of which describes a single file to be included in the request.

The `files` property is only relevant for the encoding type `json+files` where the client is expected to send a "multipart/form-data" encoded request with a set of files embedded in the request. Each part in the request should represent one file upload and the purpose of the `files` property is to describe each of these files.

Each file descriptor may specify the following properties:

* **name** [string, required]: Name of the multipart element where the file data is embedded.

* **title** [string, optional]: Title of the file.

* **description** [string, optional]: Description of the file.

* **accept** [array of string, optional]: List of accepted media types.

**Example**

This example instructs the client to send the JSON document in the part `args` and one additional file in the part `attachment`.

```
"@controls":
{
  "is:add-issue":
  {
    "title": "Add issue",
    "encoding": "json+files",
    "href": "...",
    "jsonFile": "args",
    "files": [
      {
        "name": "attachment",
        "title": "Attachment",
        "description": "Include attachment for new issue.",
        "accept": ["image/jpeg"]
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


## Property name `@error`

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

The `@error` property contains detailed error information. When a client receivers an HTTP error status code (such as 4xx or 5xx) together with a Mason document it should look for error details in the `@error` property.

Here is an example of a validation error where the end user entered a wrong value:

```json
"@error": {
  "@id": "4c4d7b1d-c76c-480e-9829-f94afed8020e",
  "@message": "There was a problem with one or more input values.",
  "@code": "INVALIDINPUT",
  "@messages": [
    "Severity should be between 1 and 5. The actual value is 30. Parameternavn: severity"
  ]
}
```

#### `@message`
This property is REQUIRED and MUST be a string value. It should be a human readable error message directed at the end user.

#### `@id` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a unique identifier for later reference to the situation that resulted in a error condition (for instance when looking up a log entry).

#### `@code` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain a code describing the error condition in general. 

#### `@messages` (optional)
This property is OPTIONAL. If present it MUST be an array of strings. It should contain an array of additional human readable error messages directed at the end user.

#### `@details` (optional)
This property is OPTIONAL. If present it MUST be a string value. It should contain an extensive human readable message directed at the client developer.

#### `@httpStatusCode` (optional)
This property is OPTIONAL. If present it MUST be a an integer value. It should contain the HTTP status code from the latest response.

#### `@controls` (optional)
This property is OPTIONAL. If present it MUST be an object adhering to the same rules as the top `@controls` object. It may contain links to resources that are relevant for the error condition. It can be links for both end users as well as client developers. A generic client won't know the difference but specific implementations can decide to use certain link relations for either of the audiences.

#### `@time` (optional)
This property is OPTIONAL. If present it MUST be a string value representing a date in the format defined by [RFC 3339](http://tools.ietf.org/html/rfc3339). Example: "1985-04-12T23:20:50.52Z". It should contain a timestamp of when the error condition occured.

