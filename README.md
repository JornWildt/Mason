# Mason: a hypermedia enabled JSON format

Mason is a JSON format for introducing hypermedia elements to classic JSON data representations. 

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
  // Additional hypermedia
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

In the above example you can see how one resource can link to other related resources using the reserved name @links. Mason works by taking a classic JSON representation and adding hypermedia and other elements to it. Mason uses the prefix @ in order to avoid name collisions between data and reserved names.

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

Download the generic client for improved browsing experience: ...

Explore the demo implementation of a fictive issue tracker service: ...

Read the specification: ...

View the source code of client and server reference implementations: https://github.com/JornWildt/Mason


## Media type registration

Mason uses the mediatype identifier `application/mason+json`. A registration with IANA is planned when Mason has a decent written spec.


## Progressive adoption


## Feedback

Discussion group for general discussion and announcements: ...

Issue tracker on GitHub: ...

Contact author: 
- Name: Jørn Wildt
- E-mail: jw@fjeldgruppen.dk
- Twitter: @JornWildt
- Blog: soabits.blogger.com


## Comparing to other hypermedia formats

Mason is not the only format for hypermedia enabled APIs. Actually it is heavily inspired by existing formats such as HAL which it owns a lot to. Here is a short comparison to some of the other formats:

[HAL](http://tools.ietf.org/html/draft-kelly-json-hal-06): Mason is a superset of HAL. Both formats works by merging raw API data with additional hypermedia elements. Mason adds actions, error handling .

[Siren](https://github.com/kevinswiber/siren): Siren and Mason are somewhat related as both of them has both links and actions. Mason adds error handling 

(*) and has a strong focus on communication with the client developers
