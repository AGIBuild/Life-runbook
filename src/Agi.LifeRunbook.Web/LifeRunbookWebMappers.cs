using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using Agi.LifeRunbook.Books;

namespace Agi.LifeRunbook.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class LifeRunbookWebMappers : MapperBase<BookDto, CreateUpdateBookDto>
{
    public override partial CreateUpdateBookDto Map(BookDto source);

    public override partial void Map(BookDto source, CreateUpdateBookDto destination);
}
