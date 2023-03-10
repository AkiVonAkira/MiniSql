<h2 align="center"><u>MiniSql</u></h2>

<h4 align="center"> A Postgres SQL Console Application </h4>

<p align="center">
<br>
    <img src="https://img.shields.io/badge/Author-AkiVonAkira-magenta?style=flat-square">
    <img src="https://img.shields.io/badge/Written%20In-CSharp-blue?style=flat-square">
</p>

### [+] Description

Simple Postgres SQL Application that allows you to view, create and modify a person, the project it has and the hours in the project.

<br>

### [+] Key Features

- Create Person and Project
- Assign Project to a Person
- Update the hours a Person has spent on a Project
- Write in menu options

<br>

### [+] Code

| **Class**             | **Breakdown**                                                                                                            |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Program.cs            | Initialize Program and run majority of functions.                                                                        |
| Menu.cs               | Contains the Menu initialization and update method, alongside cursor positioning and functionality to write in the menu. |
| Helper.cs             | Contains methods for repeaed code use.                                                                                   |
| Creator.cs            | External class where you create Person or Project.                                                                       |
| InputHandler.cs       | Two methods to read key inpu, one general method and another just for numbers (int).                                     |
| PostgresDataAccess.cs | Contains queries and methotds to talk to the Postgres Database.                                                          |
