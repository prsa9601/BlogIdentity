using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Category.Delete
{
    public record class DeleteCategoryCommand(long categoryId) : IBaseCommand;
}
