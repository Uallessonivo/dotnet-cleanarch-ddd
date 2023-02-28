using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.MenuAggregate;
using MediatR;
using OneOf;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommand
    (string HostId, string Name, string Description, List<MenuSectionCommand> Sections) : IRequest<OneOf<Menu, Errors>>;

public record MenuSectionCommand(string Name, string Description, List<MenuItemCommand> Items);

public record MenuItemCommand(string Name, string Description);