all:
	dotnet build

clean:
	dotnet clean

run:
	dotnet run -p Server/WebServiceGilBT.csproj

publish:
	cd Server
	dotnet publish -c Release 
	cd ..

	printf "Now you can copy ./Server/bin/Release/netcoreapp3.1/publish to server"

tags:
	ctags
