# Items
It's a project created for recruitment purposes 

There are some notes and comments, I'd like to leave here, about the development. 

1. Solution is containerized. Dockerfile builds an image with the app and docker-compose file sets up the environment with the app image and postgreSQL image. 
2. Items.API project has the user secrets file created for it and the directory containing the secrets is mapped to the secrets directory inside the app container.
    So that needs a correct GUID in docker-compose.yml for the app to work correctly. Secrets keep the key for JWT creation and Db connection string.
3. There are no migrations so the schema updates are made by dropping the DB and creating a new one with
```
itemsDbContext.Database.EnsureDeleted();
itemsDbContext.Database.EnsureCreated();
```
As this is called everytime when `docker compose up` is called, removing `itemsDbContext.Database.EnsureDeleted();` will make the DB container use it's assigned Docker volume for data storage.
Useful in development when the app is updated without changing the schema.

4. There is a very basic JWT with role generation, so I could authorize the endpoints, but this is absolutely not a correct way of implementing user authentication.
I left a comment about this [UsersController.cs](https://github.com/rummic/Items/blob/master/Items.API/Controllers/UsersController.cs)
5. To check performance of handling bigger loads of data I've added 5.000.000 Items to the DB in 10 batches of 500.000 records. Each insert took around 50 seconds to finish.
These are times of API calls with 5.000.000 records in the DB.

- Get top 20 Items ![image](https://user-images.githubusercontent.com/33010319/217600362-7cb57ef7-e131-4b51-9b2c-88b4ced3f84e.png)
- Get bottom 20 Items ![image](https://user-images.githubusercontent.com/33010319/217600531-b21c68cc-c4cd-4417-a1f5-13befb4ef3bf.png)
- Get middle 20 Items ![image](https://user-images.githubusercontent.com/33010319/217600612-56b91f53-7220-4e05-8916-be43cc1ed2ff.png)
- Get top 166000 items ![image](https://user-images.githubusercontent.com/33010319/217607249-39a7d3ed-bb57-4583-9d6d-17be656f3b24.png)
- Add 5000001st item ![image](https://user-images.githubusercontent.com/33010319/217600758-e8757665-c3c3-4f6e-a6dc-b5cbee25b88b.png)

Also this is the volume used by the DB with all the records in it - ![docker volume size with 5mln in db](https://user-images.githubusercontent.com/33010319/217601067-4c5288d1-6188-423d-a722-5d0092e54eb4.png)

6. Here's the list of things I would do differently if I had more time 
- Make smaller commits and not push them directly to master but use feature branches to mimic real life development scenario
- Add more unit tests. These existing are just an example of how it would look like. 
- Add DTOs for the endpoints to return. Not every model property needs to be returned.
- Tune down access modifiers, some setters could be private, methods internal and so on.
- Add more `///summary` notes to more classes like the ones I've added to [Color.cs](https://github.com/rummic/Items/blob/master/Items.Data/Model/Color.cs)
- Add logger to trace executed code.
- Move error messages from compiled strings to the other source that would allow changing them without recompiling the app like .json file or DB table. 
