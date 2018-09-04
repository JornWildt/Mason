# Mason: a hypermedia enabled JSON format

Mason is a JSON format for introducing hypermedia elements to classic JSON data representations. With Mason you get hypermedia elements for linking and modifying data, features for communicating to client developers and standardized error handling. Mason is built on JSON, reads JSON, writes JSON and generally fits well into a JSON based eco-system.

Here is a simple example representing a single issue from an example issue tracker application:

```
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
      "@controls": {
        "self": {
          "href": "http://issue-tracker.org/attachments/1"
        }
      }
    }
  ],
  // Additional hypermedia elements
  "@controls": {
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

In the above example you can see how one resource can link to other related resources using the reserved name @controls. 
Mason works by taking a classic JSON representation and adding hypermedia and more to it. 
The format uses the prefix @ in order to avoid name collisions between data and reserved names.

Mason is not restricted to links as the only kind of hypermedia element - with the @controls element it is also possible to 
represent create/update/delete/search and more complex bussiness operations. 


## Reasons for using Mason

You should be using Mason because:

- It enables building of standardized hypermedia enabled services.
- It is very easy to adopt in existing solutions.
- It is completely based on JSON(*) and fits easily into existing JSON based ecosystems.
- It enables client developers to explore all parts of a service (including all types of CRUD operations) with the use of a generic client.
- It has elements for communicating helpful information to client developers.
- It allows clients to request compact representations for improved performance(**).
- It removes client coupling on technical details such as URLs and HTTP method.
- It features standardized error handling.

(*) Except for uploading of files where multipart/form-data is used in addition to JSON.

(**) A client can ask for a minimal representation using the "Prefer: representation=minimal" HTTP header.


## Getting started

- Check the [feature list](https://github.com/JornWildt/Mason/wiki/Mason-features).
- Download the [generic client](https://github.com/JornWildt/Mason/wiki/Generic-Mason-browser) for improved browsing experience.
- Explore the [demo implementation](https://github.com/JornWildt/Mason/wiki/Example-service%3A-issue-tracker) of a fictitious issue tracker service.
- Read the [format specification](https://github.com/JornWildt/Mason/wiki/mason-format-specification).
- Sign up for the [discussion group](https://groups.google.com/d/forum/mason-media-type).
- Check out the [Mason Cookbook](https://github.com/JornWildt/MasonCookBook).

## Known implementations

- PHP server side builder [Mason-PHP](https://github.com/Phone-com/mason-php)
- WIP/Rails server side implementation [AMSA_Mason](https://github.com/mooreniemi/amsa-mason)
- .NET client [API Explorer](https://github.com/JornWildt/Mason/wiki/Generic-Mason-browser)
- .NET service side builder [MasonBuilder.Net](https://github.com/JornWildt/Mason/tree/master/MasonBuilder.Net)

## Media type registration

Mason is registered in the IANA media type registry as `application/vnd.mason+json`. See http://www.iana.org/assignments/media-types/application/vnd.mason+json


## Feedback

A discussion group for general discussion and announcements is available via Google Groups:
- https://groups.google.com/d/forum/mason-media-type
- mason-media-type@googlegroups.com

Issue tracker on GitHub: https://github.com/JornWildt/Mason/issues

You may also contact the author, Jørn Wildt, directly: 
- E-mail: jw@fjeldgruppen.dk
- Twitter: @JornWildt
- Blog: soabits.blogger.com


## Comparing to other hypermedia formats

Mason is not the only format for hypermedia enabled APIs. Actually it is heavily inspired by existing formats such as HAL which it owes a lot to. Here is a short comparison to some of the other formats:

[HAL](http://tools.ietf.org/html/draft-kelly-json-hal-06): Mason builds on the ideas from HAL. Both formats works by merging raw API data with additional hypermedia elements. Mason adds complex hypermedia elements, error handling and more (see below).

[Siren](https://github.com/kevinswiber/siren): Siren and Mason are somewhat related as each of them have both links and actions - but Siren requires special structuring of data. Mason adds error handling and more (see below).

[Hydra](http://www.markus-lanthaler.com/hydra/): Hydra is for JSON-LD (JSON linked data) and uses a completely different approach to encoding hypermedia (RDF). I cannot say how much these two formats overlap in terms of features.

[Collection-JSON](http://amundsen.com/media-types/collection/): Cj is a format for working with collections of data. Mason and Cj both have support for links, link templates, actions and error handling. However, Cj requires data to be represented as collections whereas Mason has no such kind of restrictions. Furthermore, Mason works with existing API data whereas Cj requires data to be structured in a unique way.

HTML: HTML has support for links and actions but is restricted to POST actions. Mason goes beyond that and is much more focused on APIs.

The major differentiators between Mason and the other formats are:

- Mason has a strong focus on communicating details about the API to the client developers.
- Mason uses JSON for sending data in actions (and defines a way to combine file uploads with JSON).
- Mason defines a way to reduce the payload for machine-to-machine (M2M) communication.
- Mason has a predefined set of properties for error handling.

See also why phone.com chose Mason over the other types: https://www.phone.com/blog/learning/2015/10/08/selecting-data-type-for-restful-api/
