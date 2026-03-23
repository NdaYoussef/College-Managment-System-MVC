using Microsoft.AspNetCore.Mvc;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Application.Interfaces;

[Route("Admin/Management")]
public class AdminController : Controller
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetAllUsers();
        var users = (result.Data as IEnumerable<UserInfoDto>) ?? new List<UserInfoDto>();
        return View("Users", users);
    }

    [Route("Users")]
    public async Task<IActionResult> Users()
    {
        var result = await _userService.GetAllUsers();
        var users = (result.Data as IEnumerable<UserInfoDto>) ?? new List<UserInfoDto>();
        return View(users); 
    }

    [Route("Details/{id}")]
    public async Task<IActionResult> UserDetails(string id)
    {
        var result = await _userService.GetUserData(id);
        return View(result);
    }

    [HttpPost]
    [Route("Details/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        TempData["Success"] = "User deleted successfully!";
        return RedirectToAction("Users");
    }

    [HttpPost]
    [Route("ChangeRole")]
    public async Task<IActionResult> ChangeRole(string id, string newRole)
    {
        await _userService.ChangeUserRoleAsync(id, newRole);
        TempData["Success"] = $"Role updated to {newRole} successfully!";
        return RedirectToAction("Users");
    }
}