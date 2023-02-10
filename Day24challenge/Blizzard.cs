namespace Day24challenge
{
    internal abstract class Blizzard
    {
        internal abstract Position ComputePositionAtMinute(int minute);

        internal static int ComputeMirroredInnerPosition(int innerPosition, int innerRowOrCollumnSize)
        {
            return innerRowOrCollumnSize - 1 - innerPosition;
        }
    }

    internal class BlizzardMovingRight : Blizzard
    {
        private readonly int innerStartXposition;
        private readonly int innerRowSize;
        private readonly int yPosition;

        internal BlizzardMovingRight(int startXposition, int yPosition, int rowSize) 
        {
            innerStartXposition = startXposition - 1;
            innerRowSize = rowSize - 2;
            this.yPosition = yPosition;
        }

        internal override Position ComputePositionAtMinute(int minute)
        {
            int xPosition = 1 + (innerStartXposition + minute) % innerRowSize;
            return new(xPosition, yPosition, minute);
        }
    }

    internal class BlizzardMovingDown: Blizzard
    {
        private readonly int innerStartYposition;
        private readonly int innerCollumnSize;
        private readonly int xPosition;

        internal BlizzardMovingDown(int startYposition, int xPosition, int collumnSize)
        {
            innerStartYposition = startYposition - 1;
            innerCollumnSize = collumnSize - 2;
            this.xPosition = xPosition;
        }

        internal override Position ComputePositionAtMinute(int minute)
        {
            int yPosition = 1 + (innerStartYposition + minute) % innerCollumnSize;
            return new(xPosition, yPosition, minute);
        }
    }

    internal class BlizzardMovingLeft : Blizzard
    {
        private readonly int mirroredInnerStartXposition;
        private readonly int innerRowSize;
        private readonly int yPosition;

        internal BlizzardMovingLeft(int startXposition, int yPosition, int rowSize)
        {
            int innerStartXposition = startXposition - 1;
            innerRowSize = rowSize - 2;
            mirroredInnerStartXposition = ComputeMirroredInnerPosition(innerStartXposition, innerRowSize);
            this.yPosition = yPosition;
        }

        internal override Position ComputePositionAtMinute(int minute)
        {
            int mirroredInnerXposition = (mirroredInnerStartXposition + minute) % innerRowSize;
            int innerXposition = ComputeMirroredInnerPosition(mirroredInnerXposition, innerRowSize);
            int xPosition = 1 + innerXposition;
            return new(xPosition, yPosition, minute);
        }
    }

    internal class BlizzardMovingUp : Blizzard
    {
        private readonly int mirroredInnerStartYposition;
        private readonly int innerCollumnSize;
        private readonly int xPosition;

        internal BlizzardMovingUp(int startYposition, int xPosition, int collumnSize)
        {
            int innerStartYposition = startYposition - 1;
            innerCollumnSize = collumnSize - 2;
            mirroredInnerStartYposition = ComputeMirroredInnerPosition(innerStartYposition, innerCollumnSize);
            this.xPosition = xPosition;
        }

        internal override Position ComputePositionAtMinute(int minute)
        {
            int mirroredInnerYposition = (mirroredInnerStartYposition + minute) % innerCollumnSize;
            int innerYposition = ComputeMirroredInnerPosition(mirroredInnerYposition, innerCollumnSize);
            int yPosition = 1 + innerYposition;
            return new(xPosition, yPosition, minute);
        }
    }
}
