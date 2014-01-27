# Mason: a hypermedia enabled JSON format

Mason is a JSON format for introducing hypermedia elements to classic JSON data representations. With Mason you get hypermedia elements for linking and modifying data, features for communicating to client developers and standardized error handling. Mason is build on JSON, reads JSON, writes JSON and generally fits well into a JSON based eco-system.

Here is a simple example representing a single issue from an issue tracker application:

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

In the above example you can see how one resource can link to other related resources using the reserved name @links. Mason works by taking a classic JSON representation and adding hypermedia and other elements to it. The format uses the prefix @ in order to avoid name collisions between data and reserved names.

Mason is not restricted to links as the only kind of hypermedia element but includes also actions for modifying data.


## Reasons for using Mason

You should be using Mason because:

- It enables building of standardized hypermedia enabled services.
- It is very easy to adopt in existing solutions.
- It is completely based on JSON(*) and fits easily into existing JSON based ecosystems.
- It enables client developers to explore all parts of a service (including actions) with the use of a generic client.
- It has elements for communicating helpful information to client developers.
- It allows clients to request compact representations for improved performance(**).
- It removes client coupling on technical details (such as URLs and HTTP method).
- It features standardized error handling.

(*) Except for uploading of files where multipart/form-data is used in addition to JSON.

(**) A client can ask for a minimal representation using the "Prefer: representation=minimal" HTTP header.


## Getting started

Check the [feature list](https://github.com/JornWildt/Mason/wiki/Mason-features).

Download the [generic client](https://github.com/JornWildt/Mason/wiki/Generic-Mason-browser) for improved browsing experience.

Explore the [demo implementation](https://github.com/JornWildt/Mason/wiki/Example-service%3A-issue-tracker) of a fictive issue tracker service.

Read the [format specification](https://github.com/JornWildt/Mason/wiki/mason-format-specification).

View the source code of client and server reference implementations here on GitHub.


## Media type registration

Mason uses the mediatype identifier `application/vnd.mason+json`. A registration with IANA is planned when Mason has a decent written spec.


## Feedback

Discussion group for general discussion and announcements:
- https://groups.google.com/d/forum/mason-media-type
- mason-media-type@googlegroups.com

Issue tracker on GitHub: https://github.com/JornWildt/Mason/issues

Contact author: 
- Name: Jørn Wildt
- E-mail: jw@fjeldgruppen.dk
- Twitter: @JornWildt
- Blog: soabits.blogger.com


## Comparing to other hypermedia formats

Mason is not the only format for hypermedia enabled APIs. Actually it is heavily inspired by existing formats such as HAL which it owns a lot to. Here is a short comparison to some of the other formats:

[HAL](http://tools.ietf.org/html/draft-kelly-json-hal-06): Mason is a superset of HAL. Both formats works by merging raw API data with additional hypermedia elements. Mason adds actions, error handling and more (see below).

[Siren](https://github.com/kevinswiber/siren): Siren and Mason are somewhat related as each of them have both links and actions. Mason adds error handling and more (see below).

[Hydra](http://www.markus-lanthaler.com/hydra/): Hydra is for JSON-LD (JSON linked data) and uses a completely different approach to encoding hypermedia (RDF). I cannot say how much these two formats overlap in terms of features.

[Collection-JSON](http://amundsen.com/media-types/collection/): Cj is a format for working with collections of data. Mason and Cj has both support for links, link templates, actions and error handling but Cj requires data to be represented as collections whereas Mason has no such kind of restrictions. Mason works with existing API data whereas Cj requires data to be encoded in a completely different way.

HTML: HTML has support for links and actions but is restricted to POST actions. Mason goes beyond that and is much more focused on APIs.

The major differentiators between Mason and the other formats are:

- Mason has a strong focus on communicating details about the API to the client developers.
- Mason uses JSON for sending data in actions (and defines a way to combine file uploads with JSON).
- Mason defines a way to reduce the payload for M2M communication.
- Mason has a predefined set of properties for error handling.
