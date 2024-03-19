using E_Commerce.Server.Data;
using E_Commerce.Server.Entities;
using E_Commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Server.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db)
    {
        _db = db;        
    }
    public async Task<User> RegisterAsync(User user)
    {
        var userInDb = await _db.Users.
                                 SingleOrDefaultAsync(u => u.UserEmail == user.UserEmail);
        if (userInDb == null)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }

        throw new InvalidOperationException("User already exists. Please try logging in.");
    }
    public async Task<User> LoginAsync(string password, string email)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.UserEmail == email);
        if (user == null)
        {
            throw new InvalidOperationException("User doesn't exist. Please sign up.");
        }
        else
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
            else
            {
                throw new InvalidOperationException("Invalid password.");
            }
        }
    }


    public async Task DeleteUserAsync(int id)
    {
        var userToDelete = await _db.Users.
                                     SingleOrDefaultAsync(u=>u.Id == id);
        if (userToDelete == null) 
        {
            throw new InvalidOperationException("User doesn't exist in Database");
        }
        else
        {
            _db.Users.Remove(userToDelete);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
       return await _db.Users
                        .Include(u=>u.Products)
                        .ThenInclude(p=>p.Photos)
                        .ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var userFromDb = await _db.Users
                                  .Include(u=>u.Products)
                                  .ThenInclude(p=>p.Photos)
                                  .FirstOrDefaultAsync(u=>u.Id==id);
        if(userFromDb is not  null)
        {
            return userFromDb;
        }
        throw new InvalidOperationException("User doesn't exist in database");
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var userFromDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
        if (userFromDb is not null)
        {
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.UserPhoto = user.UserPhoto;
            userFromDb.UserEmail = user.UserEmail;
            userFromDb.UpdatedAt = DateTime.Now;

            try
            {
                await _db.SaveChangesAsync();
                return userFromDb;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Reload the entity from the database
                await _db.Entry(userFromDb).ReloadAsync();

                // Apply the changes again
                userFromDb.FirstName = user.FirstName;
                userFromDb.LastName = user.LastName;
                userFromDb.UserPhoto = user.UserPhoto;
                userFromDb.UserEmail = user.UserEmail;
                userFromDb.UpdatedAt = DateTime.Now;
                await _db.SaveChangesAsync();
                return userFromDb;
            }
        }
        else
        {
            throw new InvalidOperationException("User doesn't exist");
        }
    }

}
