# Project Title
Calendar
## Description
A sample .net 5 web api project to provide api's to search and store the calendar events. It includes an in-memory database to store the events and used ef core 5 to insert and retreive the data.  
The solution also include a unit test project using xunit framework, Moq, autofixture.
### How to use it
1. Download the docker image from url  https://hub.docker.com/r/onkar1990/calendarapi/ or  by running: `docker pull onkar1990/calendarapi` .
2. Run the docker image: ` docker -run -p {port}:80 -e ASPNETCORE_ENVIRONMENT = "Development" -name {containerName} onkar1990/calendarapi`
3. To get description about the api go to https://localhost:{port}/swagger . This web page will give the information required to us the api
### Notes: There is some ssl issues while running the docker image in the production environment. Please use the above command to run the docker image.
## Authors
Onkar Singh Taneja
## Version History
V1.0
