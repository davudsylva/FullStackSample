# The sample API attempts to show best practices for a light-weight API. The code separates the data, 
# business and API layers. The layout is now:
API -  The pulic interface
Core - The business logic
Contracts - The models and such, this prevents circular references
Data - The interacts with the data store

Two types of tests are provided - these are not comprehensive but give you a taste:
* Integration tests - these would execute on a temporary database and stubbed external systems
* Unit tests - these execute against mocks to test specific tests

Bonuses:
* Swagger
* Global execution handling
* Various bug fixes

What would I add in a real production system:
* The code is structured to be simple. In a real system, I would move all logic out of the API 
  so as to keep the API as thin as possible and the buess layer to be as testable as possible.
* Validation errors would be returned as a dedicated response if a UI required it. I usually return
  a 400 with the error in the body. This would allow a user interface to display a meaningful error.
* Logging of each call, not just exceptions
* Performance monitoring - either hand-made or something like New Relic.
* Health checks - needed to detect sick machines
* Deployment pipeline
* Log shipping
etc....



