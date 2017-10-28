export class PagedData<TData> {
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    data: TData[];
}