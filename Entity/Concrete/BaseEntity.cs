using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Entity Is Active
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
 