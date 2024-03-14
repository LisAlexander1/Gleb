using System.ComponentModel.DataAnnotations.Schema;
using Gleb.Models.Interfaces;

namespace Gleb.Models.Messages;

public class SelectableItem : ISelectable
{
    [NotMapped]
    public bool IsSelected { get; set; } = false;
}