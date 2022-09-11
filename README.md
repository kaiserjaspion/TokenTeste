# TokenTeste
Project for Cashless Token implementation on microservices architecture 

Using .NET 6 (LTS), implementation don't use minimal API. Code review and lines in archive where considered to take that decision. 
Implementation use all layers with a mock context that can be replaced by another DB by desire. It uses Open API ( old Swagger ) as documentation been 
deployed as "{URL}/swagger/index.html" 

Mock implementation is inside the Context Class , bacause its never used as a proper context. On a update that changes this situation. move the mock context to the text implementation. 

Test implementation using Fact in xUnit.
