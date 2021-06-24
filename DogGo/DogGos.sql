
                        Select d.Id, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.Name as OwnerName
                        From Dog d Left Join Owner o on d.OwnerId = o.id
                        Where d.Id = 2