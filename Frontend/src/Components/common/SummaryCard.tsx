import type { LucideIcon } from "lucide-react";
import { Card, CardContent } from "../ui/card";


type Props = {
  title: string;
  value: number;
  icon: LucideIcon;
};

export function SummaryCard({ title, value, icon: Icon }: Props) {
  return (
    <Card className="rounded-2xl shadow-sm">
      <CardContent className="flex items-center justify-between p-6">
        <div>
          <p className="text-sm text-slate-500">{title}</p>
          <p className="mt-2 text-3xl font-semibold">{value}</p>
        </div>
        <div className="rounded-2xl bg-slate-100 p-3">
          <Icon className="h-6 w-6" />
        </div>
      </CardContent>
    </Card>
  );
}