using MyColor.Domain.Validation;

namespace MyColor.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        public Entity()
        { } // Necessary because of AutoMapper 

        public Entity(int id)
        {
            ValidateId(id);
        }

        private void ValidateId(int id)
        {
            DomainExceptionValidation.When(id < 1,
                   $"Invalid id: {id}. Id must be greater than 0.");

            Id = id;
        }
    }
}
