# FutePerson

Projeto iniciado dia 02/06/2022
Programadores idealizadores do projeto: João Diego, Thiago Degobi
Programadores participantes:

## Requirimento : 

Instalar o tool
```
	dotnet tool install --global dotnet-ef

```


## Comandos basicos das Migrations

criando a  primeira migration:
```
$ dotnet ef migrations add InitialCreate
```

para adicionar migration:
```
$ dotnet ef migrations add addnomemigration
```


Criando as tabelas
```
$ dotnet ef database update
```   

desfazendo:
```
$ dotnet ef migrations remove
```

listando:
```
$ dotnet ef migrations list
```