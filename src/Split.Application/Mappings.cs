using Mapster;
using Split.Application.Commands;
using Split.Domain.Models;

namespace Split.Application
{
    public static class Mappings
    {
        public static void Configure()
        {
            TypeAdapterConfig<SaveExpense, Expense>
                .NewConfig()
                .Map(dest => dest.Value, src => Money.InEuro(src.Value));
        }
    }
}