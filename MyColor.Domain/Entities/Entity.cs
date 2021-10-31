using MyColor.Domain.Validation;

namespace MyColor.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; private set; }

        public Entity(int id)
        {
            ValidateId(id);
        }

        private void ValidateId(int id)
        {
            DomainExceptionValidation.When(id < 0,
                   $"Invalid id: {id}. Id must be greater or equals 0.");

            Id = id;
        }
    }
}
