export function LoadingState({ message = "Loading..." }: { message?: string }) {
  return (
    <div className="rounded-xl border border-slate-200 bg-slate-50 px-4 py-3 text-sm text-slate-600">
      {message}
    </div>
  );
}