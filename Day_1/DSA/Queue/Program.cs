using System.ComponentModel;
 
class Program
{
    static void Main(string[] args)
    {
        ArrayDeque deque = new ArrayDeque(5);
        deque.InsertRear(10);
        deque.InsertRear(20);
        deque.InsertFront(5);
        deque.InsertRear(30);
 
        Console.WriteLine("Deque contents:");
        deque.Display();
 
        Console.WriteLine("Deleting from front: " + deque.DeleteFront());
        Console.WriteLine("Deleting from rear: " + deque.DeleteRear());
 
        Console.WriteLine("Deque after deletions: ");
        deque.Display();
 
    }
}
 
class ArrayDeque
{
    private int[] deque;
    private int front, rear, size, capacity;
 
    public ArrayDeque(int capacity)
    {
        this.capacity = capacity;
        deque = new int[capacity];
        front = -1;
        rear = -1;
        size = 0;
    }
    public bool IsFull() => size == capacity;
    public bool IsEmpty() => size == 0;
 
    public void InsertFront(int value)
    {
        if (IsFull())
            throw new InvalidOperationException("Deque Overflow");
        if (front == -1)
        {
            front = 0;
            rear = 0;
        }
        else
        {
            front = (front - 1 + capacity) % capacity;
        }
        deque[front] = value;
        size++;
    }
 
    public void InsertRear(int value)
    {
        if (IsFull())
            throw new InvalidOperationException("Deque Overflow");
        if (rear == -1)
        {
            front = 0;
            rear = 0;
        }
        else
        {
            rear = (rear + 1) % capacity;
        }
        deque[rear] = value;
        size++;
    }
 
    public int DeleteFront()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Deque Underflow");
        int value = deque[front];
 
        //only one element
        if (front == rear)
        {
            front = -1;
            rear = -1;
        }
        else
            front = (front + 1) % capacity;
        size--;
        return value;
    }
 
    public int DeleteRear()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Deque Underflow");
        int value = deque[rear];
 
        //only one element
        if (front == rear)
        {
            front = -1;
            rear = -1;
        }
        else
            rear = (rear - 1 + capacity) % capacity;
 
        size--;
        return value;
    }
 
    public void Display()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Deque is empty");
            return;
        }
 
        int i = front;
        while (true)
        {
            Console.Write(deque[i] + " ");
            if (i == rear) break;
            i = (i + 1) % capacity;
        }
        Console.WriteLine();
    }
}