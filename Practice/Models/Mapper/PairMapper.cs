using Microsoft.EntityFrameworkCore;

namespace Practice.Models.Mapper
{
    public class PairMapper : IMapper<Pair, PairDTO>
    {
        public PairDTO Map(Pair data)
        {
            return new PairDTO
            {
                PairsId = data.PairsId,
                FirstPersonId = data.FirstPersonId,
                SecondPersonId = data.SecondPersonId,
                Data = data.Data,
                FirstPersonComment = data.FirstPersonComment,
                SecondPersonComment = data.SecondPersonComment
            };
        }

        public Pair? Unmap(PairDTO data)
        {
            using (var context = new PracticeContext())
            {
                var personFirst = context.People.Where(x => x.PersonId == data.FirstPersonId).FirstOrDefault();
                var personSecond = context.People.Where(x => x.PersonId == data.SecondPersonId).FirstOrDefault();
                
                if (personFirst != null && personSecond != null) {
                    return new Pair
                    {
                        PairsId = data.PairsId,
                        FirstPersonId = data.FirstPersonId,
                        SecondPersonId = data.SecondPersonId,
                        Data = data.Data,
                        FirstPersonComment = data.FirstPersonComment,
                        SecondPersonComment = data.SecondPersonComment,
                        FirstPerson = null,
                        SecondPerson = null
                    };
                }

                return null;
            }
        }
    }
}
