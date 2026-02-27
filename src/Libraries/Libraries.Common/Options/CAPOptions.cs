using System.ComponentModel.DataAnnotations;

namespace Libraries.Common.Options;

public class CAPOptions
{
    [Required(ErrorMessage = "DefaultGroupName is required.")]
    public string DefaultGroupName { get; set; }

    [Required(ErrorMessage = "RabbitMQ configuration is required.")]
    public RabbitMQOptions RabbitMQ { get; set; }
}

public class RabbitMQOptions
{
    [Required(ErrorMessage = "RabbitMQ HostName is required.")]
    public string HostName { get; set; }

    [Required(ErrorMessage = "RabbitMQ UserName is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "RabbitMQ Password is required.")]
    public string Password { get; set; }
}