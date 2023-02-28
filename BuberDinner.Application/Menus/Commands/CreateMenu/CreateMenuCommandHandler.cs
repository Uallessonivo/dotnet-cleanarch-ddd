using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using MediatR;
using OneOf;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, OneOf<Menu, Errors>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<OneOf<Menu, Errors>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Create Menu
        var menu = Menu.Create(HostId.CreateUnique(), request.Name, request.Description,
            request.Sections.ConvertAll(section => MenuSection.Create(section.Name, section.Description,
                section.Items.ConvertAll(item => MenuItem.Create(item.Name, item.Description)))));
        
        // Persist Menu
        _menuRepository.Add(menu);
        
        // Return Menu
        return menu;
    }
};