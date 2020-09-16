# GraphQL

This GraphQL implementation uses [GraphiQL ("graph-ih-cle")](https://github.com/graphql/graphiql) as a UI for exploring/testing the GraphQL API. The endpoint for the UI is `GET /graphql` while all GraphQL queries will use `POST /graphql`.

The available queries and their schema can be  via [GraphQL introspection](https://graphql.org/learn/introspection/). An example schema query is:

```
{
  __schema {
    queryType {
      name
      kind
      fields {
        name
        type {
          name
          kind
        }
      }
    }
  }
}
```


## Example queries

```
query UserQuery($id: Int!) {
  user(id: $id) {
    id
    city
    firstName
    lastName
  }
}

query ProductQuery($id: Int!) {
  product(id: $id) {
    id
    name
    price
  }
}

query OrderQuery($id: Int!) {
  order(id: $id) {
    id
    date
    userId
    items {
      quantity
      productId
      product {
        name
        price
      }
    }
  }
}
```

