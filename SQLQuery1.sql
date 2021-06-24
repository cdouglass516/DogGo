Select w.[Id] ,w.[Date],w.[Duration],w.[WalkerId],w.[DogId], d.Name as DogName, o.Name as OwnerName
                        From Walks w 
                        join Dog d on w.DogId = d.ID 
                        Join Owner o on d.OwnerId = o.id
                        Where w.WalkerId = 1