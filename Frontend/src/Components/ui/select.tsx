import * as React from "react";
import { cn } from "../../lib/utils";

type SelectContextType = {
  value?: string;
  onValueChange?: (value: string) => void;
};

const SelectContext = React.createContext<SelectContextType>({});

interface SelectProps {
  value?: string;
  onValueChange?: (value: string) => void;
  children: React.ReactNode;
}

function Select({ value, onValueChange, children }: SelectProps) {
  return (
    <SelectContext.Provider value={{ value, onValueChange }}>
      <div className="w-full">{children}</div>
    </SelectContext.Provider>
  );
}

interface SelectTriggerProps extends React.HTMLAttributes<HTMLDivElement> {
  children: React.ReactNode;
}

function SelectTrigger({ className, children }: SelectTriggerProps) {
  return (
    <div
      className={cn(
        "flex h-10 w-full items-center justify-between rounded-md border border-slate-300 bg-white px-3 py-2 text-sm",
        "focus-within:ring-2 focus-within:ring-slate-400",
        className
      )}
    >
      {children}
    </div>
  );
}

interface SelectValueProps {
  placeholder?: string;
}

function SelectValue({ placeholder }: SelectValueProps) {
  const { value } = React.useContext(SelectContext);

  return (
    <span className={cn(!value && "text-slate-400")}>
      {value || placeholder || "Select an option"}
    </span>
  );
}

interface SelectContentProps {
  children: React.ReactNode;
  className?: string;
}

type SelectItemElementProps = {
  value: string;
  children: React.ReactNode;
};

function SelectContent({ children, className }: SelectContentProps) {
  const { value, onValueChange } = React.useContext(SelectContext);

  const items = React.Children.toArray(children).filter(
    React.isValidElement
  ) as React.ReactElement<SelectItemElementProps>[];

  return (
    <select
      value={value ?? ""}
      onChange={(e) => onValueChange?.(e.target.value)}
      className={cn(
        "mt-2 flex h-10 w-full rounded-md border border-slate-300 bg-white px-3 py-2 text-sm",
        "focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-slate-400",
        className
      )}
    >
      {items.map((child) => {
        const { value: itemValue, children: itemChildren } = child.props;
        return (
          <option key={itemValue} value={itemValue}>
            {itemChildren}
          </option>
        );
      })}
    </select>
  );
}

interface SelectItemProps {
  value: string;
  children: React.ReactNode;
}

function SelectItem({ value, children }: SelectItemProps) {
  return <option value={value}>{children}</option>;
}

export {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
};