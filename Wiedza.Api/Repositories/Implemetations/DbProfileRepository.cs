using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Models;
using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implemetations;

public class DbProfileRepository(DataContext dataContext) : IProfileRepository
{
    public async Task<Result<EditProfileResponse>> GetEditProfileAsync(Guid idPerson)
    {
        var person = await dataContext.Persons.FirstOrDefaultAsync(x => x.Id == idPerson);
        if (person == null) return new Exception("The person is not found");
        var result = new EditProfileResponse
        {
            Username = person.Username,
            Name = person.Name,
            Email = person.Email,
            Description = person.Description,
            Avatar = person.AvatarBytes
        };
        return result;
    }
    
    
    public async Task<Result<EditProfileResponse>> ChangeEditProfileAsync(EditProfileRequest editProfileRequest)
    {
        var person = await dataContext.Persons.FirstOrDefaultAsync(x => x.Id == editProfileRequest.Id);
        if (person == null) return new Exception("The person is not exist!");

        var isUsernameExist = await dataContext.Persons.AnyAsync(x => x.Username == editProfileRequest.Username);
        if (isUsernameExist) return new Exception("The Username is exist!");
        person.Username = editProfileRequest.Username;
        person.Name = editProfileRequest.Name;
        person.Description = editProfileRequest.Description;

        dataContext.Update(person);
        await dataContext.SaveChangesAsync();

        var newPerson = new EditProfileResponse
        {
            Username = person.Username,
            Name = person.Name,
            Email = person.Email,
            Description = person.Description,
            Avatar = person.AvatarBytes
        };

        return newPerson;
    }

}