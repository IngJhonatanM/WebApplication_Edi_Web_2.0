﻿using EDIBANK.Conf_Db_With_Entity;
using EDIBANK.Models.Users_EdiWeb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDIBANK.Controllers.ManagerController;


// Instance of the ASP.NET Core Identity UserManager available in the controller
[Authorize(Roles = "Admin")]
public class ManageUsersController(AppDbContext context, UserManager<ApplicationUser> usrMgr, IPasswordHasher<ApplicationUser> passwordHash, RoleManager<IdentityRole> roleMgr) : Controller
{
    private readonly AppDbContext _context = context;

    private readonly UserManager<ApplicationUser> _userManager = usrMgr;

    private IPasswordHasher<ApplicationUser> _passwordHasher = passwordHash;

    private RoleManager<IdentityRole> _roleManager = roleMgr;

    //use the GetUserAsync method to retrieve an instance of ApplicationIdentityUser for the currently logged in user.
    //This means making the controller method asynchronous so that we can await the call to GetUserAsync 
    public async Task<IActionResult> Index()
    {
        ApplicationUser[] admin = (await _userManager
            .GetUsersInRoleAsync("Admin"))
            .ToArray();

        ApplicationUser[] everyone = await _userManager.Users
            .ToArrayAsync();

        ManageUsersViewModel model = new()
        {
            Admin = admin,
            Everyone = everyone
        };

        return View(model);
    }

    // Get all Identity Roles

    // Create User
    public async Task<IActionResult> CreateAsync()
    {
        string ediId = (await _context.EDISelectorAsync()).First().Value;

        return View(new Users
        {
            Name = "",
            Email = null,
            Password = "",
            DescripUser = null,
            EDIs = await _context.EDISelectorAsync(ediId),
            EDIId = ediId
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Users user)
    {
        if (!ModelState.IsValid)
        {
            user.EDIs = await _context.EDISelectorAsync(user.EDIId);
            return View(user);
        }

        ApplicationUser appUser = new()
        {
            UserName = user.Email,
            Email = user.Email,
            DescripUser = user.DescripUser,
            EDIId = user.EDIId,
            TwoFactorEnabled = true,
            EmailConfirmed = true,
            LockoutEnabled = true
        };
        IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

        if (result.Succeeded)
        {
            // Set the user role
            await _userManager.AddToRoleAsync(appUser, "User");

            //  var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            //  var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
            //   EmailHelper emailHelper = new EmailHelper();
            //  bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

            //  if (emailResponse)
            // return RedirectToAction("Index");
            // else
            //   {
            // log email failed 
            // }
        }
        else
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return RedirectToAction("Index");
    }

    //Update
    public async Task<IActionResult> Update(string id)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(id);
        if (user != null)
            return View(user);
        else
            return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, string email, string? password, string DescripUser)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            if (!string.IsNullOrEmpty(email))
                user.Email = email;

            if (!string.IsNullOrEmpty(DescripUser))
                user.DescripUser = DescripUser;

            else
                ModelState.AddModelError("", "DescripUser cannot be empty");

            if (!string.IsNullOrEmpty(password))

                user.PasswordHash = _passwordHasher.HashPassword(user, password);
            IdentityResult resultadoPass = await _userManager.UpdateAsync(user);

            if (resultadoPass.Succeeded)
                return RedirectToAction("Index");

            if (!string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(DescripUser))
            {
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
        }
        else
            ModelState.AddModelError("", "User Not Found");
        return View(user);
    }

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }

    // Delete

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        else
            ModelState.AddModelError("", "User Not Found");
        return View("Index", _userManager.Users);
    }

    // UnBlockUser

    //  [HttpPost]
    public async Task<IActionResult> UnblockUser(string id)

    {
        // Encuentra al usuario por su ID
        ApplicationUser user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            // Manejar el caso en que el usuario no se encuentra
            return NotFound();
        }

        // Verificar si el usuario está bloqueado
        if (await _userManager.IsLockedOutAsync(user))
        {
            // Desbloquear al usuario
            //  await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.ResetAccessFailedCountAsync(user);
            // Volver a habilitar el bloqueo para futuros intentos fallidos
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));

            // Actualizar la última fecha de inicio de sesión para reiniciar el temporizador de bloqueo
            await _userManager.UpdateSecurityStampAsync(user);

            // Agregar un mensaje de éxito o redireccionar a una página de confirmación
            return RedirectToAction("Index", "ManageUsers", new { message = "Usuario desbloqueado correctamente" });
        }
        else
        {
            // Agregar un mensaje de error indicando que el usuario no estaba bloqueado
            return RedirectToAction("Index", "ManageUsers", new { message = "El usuario ya no estaba bloqueado" });
        }
    }

}