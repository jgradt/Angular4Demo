export class PagedData<TData> {
    pageIndex: number;
    pageSize: number;
    totalPages: number;
    totalItems: number;
    data: TData[];
}