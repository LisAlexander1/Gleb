using System.ComponentModel.DataAnnotations.Schema;

namespace Gleb.Models.Interfaces;

public interface ISelectable
{
    [NotMapped]
    public bool IsSelected { get; set; }
}