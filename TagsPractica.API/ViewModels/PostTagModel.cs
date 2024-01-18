using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using TagsPractica.DAL.Models;

namespace TagsPractica.API.ViewModels
{
    public class PostTagModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
