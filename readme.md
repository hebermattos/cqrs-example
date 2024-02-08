## CQRS

### What's it?

The CQRS (Command Query Responsibility Segregation) pattern is an architectural approach that separates read operations (queries) from write operations (commands) within a system. This allows optimizing each type of operation according to its specific needs. For instance, read operations can be optimized for fast and efficient queries, while write operations may prioritize data consistency and validation. This clear separation can lead to more scalable and flexible systems, although it may add some complexity to the design and implementation.

### Two Databases?

Utilizing two databases in the CQRS pattern involves dedicating one database to handle commands and another for queries. This optimization allows each database to be tailored for its specific task, enhancing performance and scalability. The command database focuses on data consistency and validation, while the query database prioritizes fast read access, often employing denormalized structures or caching. This separation enables the system to efficiently handle varying workloads and scale independently. However, ensuring data consistency between the two databases becomes a critical consideration in the design.

### Use Cases

* Heavy Read Loads: If an application has a significant read load compared to the write load, separating the database optimized for queries from the one used for commands can improve performance.

* Independent Scalability: By separating the command and query databases, each can scale independently as needed, allocating resources where necessary without affecting the other.

* Different Performance Needs: Using different data storage technologies for read and write operations can meet specific performance requirements more effectively. For instance, a relational database may ensure data consistency during writes, while a NoSQL database may offer better read performance.

### Difficulties

* Complexity: Adopting CQRS adds complexity to system architecture and development. Managing separate models for read and write operations increases cognitive load and requires effort to maintain consistency.

* Data Consistency: Ensuring consistency between the command and query sides is challenging. Synchronizing data between the two databases may lead to eventual consistency issues or additional overhead.

* Performance Overhead: Maintaining separate read and write models, along with data synchronization, can impact system performance. Careful design and optimization are necessary to address potential performance bottlenecks.

### Example

The [docker-compose.yml](https://github.com/hebermattos/cqrs-example/blob/master/docker-compose.yml) file outlines the configuration of a Docker environment following the CQRS pattern. In the context of this pattern, commands are directed to SQL Server, while queries are processed in Elasticsearch.
In addition to the main services, there is an additional service called update-elastic. This service plays a crucial role in synchronizing Elasticsearch with SQL Server. It is responsible for updating Elasticsearch with any changes that occur in SQL Server, ensuring that the data in Elasticsearch is always up-to-date and in sync with the primary database. You can check some advantagens of using assincronous updates [here](https://medium.com/poatek/scaling-your-app-with-rabbitmq-eb9cb6c8d9d6)

### Why Elasticsearch?

* Optimized Read Performance: Elasticsearch is highly optimized for read operations, enabling fast and efficient queries on large volumes of data. This is particularly beneficial for query operations in the CQRS pattern, which are directed to Elasticsearch.

* Horizontal Scalability: Elasticsearch is highly scalable and inherently distributed, allowing infrastructure to scale horizontally as needed. This is crucial in scenarios where there is a large volume of read operations.

* Advanced Search Capabilities: Elasticsearch offers advanced search features, including full-text search, advanced filtering, aggregations, relevance analysis, and more. This makes it easier to implement complex search functionalities within the context of the CQRS pattern.