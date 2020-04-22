all:
	dotnet build

clean:
	dotnet clean

run:
	dotnet run -p Server/WebServiceGilBT.csproj

tags:
	ctags
