Run the sample


dotnet publish -c Release


docker build . -t ver1


docker run -it -p 8080:80 ver1


Navigate to http://localhost:8080/Test and refresh a few times