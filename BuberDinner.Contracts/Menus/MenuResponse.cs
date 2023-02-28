namespace BuberDinner.Contracts.Menus;

public record MenuResponse(Guid Id, string Name, string Description, float? AverageRating,
    List<MenuSectionResponse> Sections, string HostId, List<string> DinnerIds, List<string> MenuReviewIds,
    DateTime CreatedDateTime, DateTime UpdatedDateTime);

public record MenuSectionResponse(Guid Id, string Name, string Description, float? AverageRating,
    List<MenuItemResponse> Items);

public record MenuItemResponse(Guid Id, string Name, string Description, float? AverageRating);