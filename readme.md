## CQRS

### What's it?

The CQRS (Command Query Responsibility Segregation) pattern is an architectural approach that separates:

* Read operations (queries) 
* Write operations (commands)

This allows optimizing each type of operation according to its specific needs. For instance, read operations can be optimized for fast and efficient queries, while write operations may prioritize data consistency and validation. This clear separation can lead to more scalable and flexible systems, although it may add some complexity to the design and implementation.

### One Database, Logical Separation

The logical separation of the CQRS pattern simplifies system implementation by dividing reading and writing responsibilities without physically separating databases. This approach streamlines development and maintenance, reduces operational complexity, and facilitates agile system evolution. By avoiding synchronization between databases, it minimizes errors and data inconsistencies while potentially improving performance. In summary, CQRS's logical separation offers an efficient way to implement reading and writing functionalities, laying the groundwork for scalable, flexible, and easily maintained systems.

### Two Databases, Physical separation

Utilizing two databases in the CQRS pattern involves dedicating one database to handle commands and another for queries. This optimization allows each database to be tailored for its specific task, enhancing performance and scalability. The command database focuses on data consistency and validation, while the query database prioritizes fast read access, often employing denormalized structures or caching. This separation enables the system to efficiently handle varying workloads and scale independently. However, ensuring data consistency between the two databases becomes a critical consideration in the design.

### Use Cases

* Heavy Read Loads: If an application has a significant read load compared to the write load, separating the database optimized for queries from the one used for commands can improve performance.

* Scalability: By separating the command and query databases, each can scale independently as needed, allocating resources where necessary without affecting the other.

* Performance: Using different data storage technologies for read and write operations can meet specific performance requirements more effectively. For instance, a relational database may ensure data consistency during writes, while a NoSQL database may offer better read performance.

### Difficulties

* Complexity: Adopting CQRS adds complexity to system architecture and development. Managing separate models for read and write operations increases cognitive load and requires effort to maintain consistency.

* Data Consistency: Ensuring consistency between the command and query sides is challenging. Synchronizing data between the two databases may lead to eventual consistency issues or additional overhead.

* Performance Overhead: Maintaining separate read and write models, along with data synchronization, can impact system performance. Careful design and optimization are necessary to address potential performance bottlenecks.

### Example

The [docker-compose.yml](https://github.com/hebermattos/cqrs-example/blob/master/docker-compose.yml) file outlines the configuration of a Docker environment following the CQRS pattern. In the context of this pattern, commands are directed to SQL Server, while queries are processed in Elasticsearch.
In addition to the main services, there is an additional service called update-elastic. This service plays a crucial role in synchronizing Elasticsearch with SQL Server. It is responsible for updating Elasticsearch with any changes that occur in SQL Server, ensuring that the data in Elasticsearch is always up-to-date and in sync with the primary database. You can check some advantages of using asynchronous updates [here](https://medium.com/poatek/scaling-your-app-with-rabbitmq-eb9cb6c8d9d6)

### Queries - Elasticsearch

* Optimized Read Performance: Elasticsearch is highly optimized for read operations, enabling fast and efficient queries on large volumes of data. This is particularly beneficial for query operations in the CQRS pattern, which are directed to Elasticsearch.

* Horizontal Scalability: Elasticsearch is highly scalable and inherently distributed, allowing infrastructure to scale horizontally as needed. This is crucial in scenarios where there is a large volume of read operations.

* Advanced Search Capabilities: Elasticsearch offers advanced search features, including full-text search, advanced filtering, aggregations, relevance analysis, and more. This makes it easier to implement complex search functionalities within the context of the CQRS pattern.

### Commands - SQL Server

* Transactional Support: SQL Server provides a robust transactional mechanism, ensuring data consistency during complex write operations, essential for CQRS command implementation.

* Flexible Data Modeling: With SQL Server, data can be modeled flexibly, both in relational and NoSQL aspects, adapting to the specific requirements of each command or query in CQRS.

* Security and Access Control: SQL Server offers advanced security features and access control, allowing for the implementation of granular permissions to ensure that only authorized users can execute write or read operations.