export interface IOrder {
    id: number;
    customerId: number;
    deliveredDate: Date;
    orderDate: Date;
    status: string;
    totalDue: number;
    comment: string;
}
