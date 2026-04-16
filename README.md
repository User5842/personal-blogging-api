# Personal Blogging Platform API

## Functional Requirements

- Return a list of articles. You can add filters such as publishing date, or tags.
- Return a single article, specified by the ID of the article.
- Create a new article to be published.
- Delete a single article, specified by the ID.
- Update a single article, again, you’d specify the article using its ID.

## Entity Design

### Article

```
Categories: string[]
Content: string
Id: int
Description: string
PublishedAt: DateTime
Title: string
```

## API Design

GET /articles HTTP/1.1
Content-Type: application/json

HTTP/1.1 200 OK
Content-Type: application/json

[
    {
        Categories: [General],
        Content: This is the content of my first post,
        Id: 1,
        Description: This is an awesome post,
        PublishedAt: 4/15/2026,
        Title: My first post
    },
    {
        Categories: [General],
        Content: This is the content of my second post,
        Id: 2,
        Description: This is an awesome post, too,
        PublishedAt: 4/10/2026,
        Title: My second post
    }
]