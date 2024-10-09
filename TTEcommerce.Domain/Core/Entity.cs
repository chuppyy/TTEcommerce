namespace TTEcommerce.Domain.Core
{
    public abstract class Entity
    {
        public string Id { get; protected set; }

        protected Entity()
        {
            Id = StrHelper.GenRndStr(8);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
