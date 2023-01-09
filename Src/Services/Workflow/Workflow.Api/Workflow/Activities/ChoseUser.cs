using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;
using Elsa.Expressions;
using Elsa.Metadata;
using Elsa.Server.Api.Endpoints.Workflows;
using Elsa.Services;
using Elsa.Services.Models;
using Newtonsoft.Json.Linq;

namespace Workflow.Api.Workflow.Activities;

/// <summary>
/// 选择人员
/// </summary>
[Activity(Category = "Ding", Description = "选择人员", DisplayName = "选择人员")]

//[Action]
public class ChoseUser : Activity, IActivityPropertyOptionsProvider, IRuntimeSelectListProvider
{

    [ActivityInput(
            Label = "角色",
            //Hint = "角色",
            UIHint = ActivityInputUIHints.CheckList,
            DefaultSyntax = SyntaxNames.Liquid,
            OptionsProvider = typeof(ChoseUser)
            //Category = "角色", // 单独分出一列
            //DependsOnEvents = new[] { "Brand" },
            //DependsOnValues = new[] { "Brand" }
    )]
    public int? RoleId { get; set; } = 2;


    [ActivityInput(
        Label = "性别",
        UIHint = ActivityInputUIHints.RadioList,
        DefaultSyntax = SyntaxNames.Liquid,
        OptionsProvider = typeof(ChoseUser)
        //DependsOnEvents = new[] { "Model" },
        //DependsOnValues = new[] { "Model", "Brand" }
    )]
    public bool? IsMan { get; set; }


    [ActivityInput(
    Label = "用户",
    UIHint = ActivityInputUIHints.Dropdown,
    DefaultSyntax = SyntaxNames.Liquid,
    OptionsProvider = typeof(ChoseUser),
    SupportedSyntaxes = new[] { SyntaxNames.Json, SyntaxNames.Liquid }
    //DependsOnEvents = new[] { "Brand" },
    //DependsOnValues = new[] { "Brand" }
)]
    public string UserName { get; set; }

    //public object GetOptions(PropertyInfo property) => new RuntimeSelectListProviderSettings(GetType(), new VehicleContext(RoleId));

    public object GetOptions(PropertyInfo property) => new RuntimeSelectListProviderSettings(GetType(),
    new CascadingDropDownContext(property.Name,
        property.GetCustomAttribute<ActivityInputAttribute>()!.DependsOnEvents!,
        property.GetCustomAttribute<ActivityInputAttribute>()!.DependsOnValues!
        , new Dictionary<string, string>(), new VehicleContext(RoleId)));


    public record VehicleContext(int? RandomNumber);

    public record CascadingDropDownContext(string Name, string[] DependsOnEvent, string[] DependsOnValue, IDictionary<string, string> DepValues, object? Context);

    [ActivityOutput] public string? Output { get; set; }

    protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
    {
        Output = UserName;
        return Done();
    }

    protected override async ValueTask<IActivityExecutionResult> OnResumeAsync(ActivityExecutionContext context)
    {
        return Done();
    }

    /// <summary>
    /// 初始化时会触发，不会随着切换而去变动
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public ValueTask<SelectList> GetSelectListAsync(object context = null, CancellationToken cancellationToken = default)
    {
        //var vehicleContext = (VehicleContext)context!;
        //var items = arrays.Where((c,d)=>d== vehicleContext.RandomNumber).Select((x,y) => new SelectListItem { Value = y.ToString(), Text =x }).ToList();
        //return new ValueTask<SelectList>(new SelectList { Items = items });

        var cascadingDropDownContext = (CascadingDropDownContext)context!;
        var dataSource = GetDataSource();
        var dependencyValues = cascadingDropDownContext.DepValues;

        switch (cascadingDropDownContext.Name)
        {
            case "RoleId":
                {
                    var listSelect = new SelectList()
                    {
                        Items = new List<SelectListItem>
                        {  
                            new SelectListItem()
                            {
                                Text="系统管理员",
                                Value="1"
                            },
                             new SelectListItem()
                            {
                                Text="网格员",
                                Value="2"
                            },
                              new SelectListItem()
                            {
                                Text="处置人员",
                                Value="3"
                            }
                        }
                    };
                    return new ValueTask<SelectList>(listSelect);
                }
            case "IsMan": //when dependencyValues.TryGetValue("Brand", out var brandValue)
                {
                    var listSelect = new SelectList()
                    {
                        Items = new List<SelectListItem>
                        {
                            new SelectListItem()
                            {
                                Text="男",
                                Value="1"
                            },
                             new SelectListItem()
                            {
                                Text="女",
                                Value="2"
                            },

                        }
                    };
                    return new ValueTask<SelectList>(listSelect);
                }
                case "UserName"://when dependencyValues.TryGetValue("Brand", out var brandValue) && dependencyValues.TryGetValue("Model", out var modelValue)
                {
                    var listSelect = new SelectList()
                    {
                        Items = new List<SelectListItem>
                        {
                            new SelectListItem()
                            {
                                Text="张三",
                                Value="10001"
                            },
                           new SelectListItem()
                            {
                                Text="李四",
                                Value="10002"
                            },
                        }
                    };
                    return new ValueTask<SelectList>(listSelect);
                }
            default:
                return new ValueTask<SelectList>();
        }

    }

    private string[] arrays = { "BMW", "Peugot", "Tesla" };

    private JArray GetDataSource()
    {
        return JArray.FromObject(new[]
        {
                new
                {
                    Name = "BMW",
                    Models = new[]
                    {
                        new
                        {
                            Name = "1 Series",
                            Colors = new[] { "Purple Silk metallic", "Java Green metallic", "Macao Blue" }
                        },
                        new
                        {
                            Name = "2 Series",
                            Colors = new[] { "Purple Silk metallic", "Java Green metallic", "Macao Blue" }
                        },
                        new
                        {
                            Name = "i3",
                            Colors = new[] { "Purple Silk metallic", "Java Green metallic", "Macao Blue" }
                        },
                        new
                        {
                            Name = "i8",
                            Colors = new[] { "Purple Silk metallic", "Java Green metallic", "Macao Blue" }
                        }
                    }
                },
                new
                {
                    Name = "Peugeot",
                    Models = new[]
                    {
                        new
                        {
                            Name = "208",
                            Colors = new[] { "Red", "White" }
                        },
                        new
                        {
                            Name = "301",
                            Colors = new[] { "Yellow", "Green" }
                        },
                        new
                        {
                            Name = "508",
                            Colors = new[] { "Purple", "Black" }
                        }
                    }
                },
                new
                {
                    Name = "Tesla",
                    Models = new[]
                    {
                        new
                        {
                            Name = "Roadster",
                            Colors = new[] { "Purple", "Brown" }
                        },
                        new
                        {
                            Name = "Model S",
                            Colors = new[] { "Red", "Black", "White", "Blue" }
                        },
                        new
                        {
                            Name = "Model 3",
                            Colors = new[] { "Red", "Black", "White", "Blue" }
                        },
                        new
                        {
                            Name = "Model X",
                            Colors = new[] { "Red", "Black", "White", "Blue" }
                        },
                        new
                        {
                            Name = "Model Y",
                            Colors = new[] { "Silver", "Black", "White" }
                        }
                    }
                }
            });
    }
}