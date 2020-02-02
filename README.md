# Group Generator

This project was inspired by a project suggestion by Dr. Douglas Selent in the Fall of 2018. This group generator will allow professors to create project groups while considering student preferences for who they wish/don't wish to be matched with. This is my first project involving WPF in Visual Studio. The algorithm is based off a similar procedural algorithm used by clevergroups.com. Research into an NP complete variant of the [Stable Roommates Problem](https://en.wikipedia.org/wiki/Stable_roommates_problem) for n-rooms turned out to be an inefficient use of my freetime, so this is the algorithm used for now.
The program takes a CSV file as input for the roster. It should be formatted as so:

    First Name,Last Name
    FirstName,LastName
    ...
As of 02/02/2020, there is a lack of error handling for incorrect input of some parameters. I also have not implemented a satisfaction measure for the success of group forming based on preferences.