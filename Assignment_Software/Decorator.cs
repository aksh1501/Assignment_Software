namespace Decorator
{
    interface ICar
    {
        void build();

    }

    class SimpleCar : ICar
    {
        public void build()
        {
            Console.WriteLine("This is a Simple Car");
        }
    }

    abstract class CarWrapper : ICar
    {
        ICar WrappedCar;

        public CarWrapper(ICar Car) 
        {
            WrappedCar = Car;
        }

        virtual public void build()
        {
            WrappedCar.build();
        }
    }

    class FastCar : CarWrapper
    {
        public FastCar(ICar C) : base(C)
        {
        }

        override public void build()
        {
            base.build();
            FastEngine();
        }

        void FastEngine()
        {
            Console.WriteLine("A fast Engine Car");
        }
    }



    class StylishCar : CarWrapper
    {
        public StylishCar(ICar C) : base(C)
        {
        }

        override public void build()
        {
            base.build();
            StyledEngine();
        }

        void StyledEngine()
        {
            Console.WriteLine("A Styled Engine Car");
        }
    }


    interface ITransmission
    {
        void transmit(byte[] b);
        void receive();
    }

    class StandardTransmission : ITransmission
    {
        public void receive()
        {
            Console.WriteLine("Data received");
        }

        public void transmit(byte[] b)
        {
            Console.WriteLine("Transmitting bytes");
        }
    }


    abstract class TransmitionWrapper : ITransmission
    {
        ITransmission _wrapped;

        public TransmitionWrapper(ITransmission T)
        {
            _wrapped=T;
        }

        virtual public void receive()
        {
            _wrapped.receive();    
        }

        virtual public void transmit(byte[] b)
        {
            _wrapped.transmit(b);
        }
    }

    class CompressedTransmitter : TransmitionWrapper
    {

        public CompressedTransmitter(ITransmission T):  base(T)
        {
        }

        override public void receive()
        {
            base.receive();  
            Console.WriteLine("Decompressing data.."); 
        }

        override public void transmit(byte[] b)
        {
            compress();
            base.transmit(b);
        }

        void compress()
        {
            Console.WriteLine("Compressing Data..");
        }
    }


    class EncryptTransmitter : TransmitionWrapper
    {

        public EncryptTransmitter(ITransmission T):  base(T)
        {
        }

        override public void receive()
        {
            base.receive(); 
            Console.WriteLine("Decrypting Data");
        }

        override public void transmit(byte[] b)
        {
            encrypt();
            base.transmit(b);
        }

        void encrypt()
        {
            Console.WriteLine("Encrypting Data..");
        }
    }


}