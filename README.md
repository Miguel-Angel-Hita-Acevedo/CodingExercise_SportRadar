# How to run/configure locally; how to run tests.

```shell
# Open a terminal (Command Prompt or PowerShell for Windows)

# Ensure Git is installed
# Visit https://git-scm.com to download and install console Git if not already installed

# Clone the repository
git clone https://github.com/Miguel-Angel-Hita-Acevedo/CodingExercise_SportRadar.git

# Navigate to the project directory
cd SportRadarApi

# Run the api
dotnet run

# Now to run the tests
dotnet test ../SportRadarApi.Test

```

Note: Endpoint work properly with postman for example calling the POST endpoint "[urlLocalServer]/Create/AddBet" using a proper body, I deliberately left the GET endpoint on "[urlLocalServer]/Create/Bet" to get an example json that is received correctly on AddBet endpoint.

# Design decisions & trade-offs (queues, ordering, shutdown, idempotency).

## Architecture

Applied on this proyect some structure of clean architecture with layer separation.

In my opinion on most cases CQRS pattern is very useful to API proyects with db connections, in this case I mocked the db connection but the structure should be almost the same.

## Queues & Messaging

Mainly I needed the creation of Queues because have a variant that manages well the concurrency and make sure, that the request are placed in the right order having in mind possible massive requests at the same time.

Dictionary because the only field that I found with problems with the concurrency was the id because it can be repeated in multiple requests to change the state, with the search speed of Dictionaries in mind it was the final solution, is known that Dictionaries are heavy on creation and deleting elements but in my opinion, this case have more weight on search elements.

Messages are written on the common "Console.WriteLine" to write on console with the output requested.

## Ordering & Concurrency

In my opinion it was the heviest part of the project, manage multiple threads retrieving ordered output to let all the customers with a right request.

Queues was an useful tool to process the requests on the right order at first and dicitonary to separate them in matter of id to avoid data corruption on the states of the requests.

## Shutdown Behavior

As the name says, an API was implemented on this case to deploy a server in local, so I decided to implement it on the "shutdown" of the server, when it is closed and sending the summary information at this time as the exercise says.

# Assumptions & constraints you made.

On the first paragraph after "Objetives" the word concurrently was what made me think of threads in matter to optimize and avoid data corruption.

Except Id, Ammount and Status, I didn't found any references to make useful the other fields so I focused just on these fields.

The PDF says to "simulate" an API REST but I thought that could be positive on this test to do it properly with a working endpoint to add requests.

# Future improvements

The place of all the files and their names are not the best and are open to discusion.

Most of the project are pending of a huge refactor to make more parts testable, text are harcoded ...
