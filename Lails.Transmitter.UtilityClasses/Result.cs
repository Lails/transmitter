using System;
using System.Collections;

namespace Lails.Transmitter.UtilityClasses
{
	public static class Result
	{
		public static ResultOk<TValue> Ok<TValue>(TValue value) => new ResultOk<TValue>(value);
		public static ResultError<TError> Error<TError>(TError error) => new ResultError<TError>(error);
	}

	public readonly struct Result<TValue, TError>
	{
		public readonly TValue Value;
		public readonly TError Error;
		public readonly bool IsError;

		private Result(TValue value, TError error, bool isError)
		{
			Value = value;
			Error = error;
			IsError = isError;
		}

		public Result(TValue ok) : this(ok, default, false) { }
		public Result(TError error) : this(default, error, true) { }

		public static implicit operator Result<TValue, TError>(ResultOk<TValue> value) => new Result<TValue, TError>(value.Value);
		public static implicit operator Result<TValue, TError>(ResultError<TError> error) => new Result<TValue, TError>(error.Value);

		public void ThrowExceptionIfReturnsNull()
		{
			if (Value == null)
			{
				throw new Exception($"Object {Value.GetType().FullName} is null");
			}
		}
		public void ThrowExceptionIfReturnsCollectoinIsNullOrEmpty()
		{
			if (Value == null)
			{
				throw new Exception($"Collection {Value.GetType().FullName} is null");
			}

			if (Value is IEnumerable values)
			{
				bool needThrow = true;
				foreach (var value in values)
				{
					needThrow = false;
					break;
				}
				if (needThrow)
				{
					throw new Exception($"Collection {values.GetType().FullName} is empty");
				}

			}
		}
	}

	public readonly struct ResultOk<T>
	{
		public T Value { get; }

		public ResultOk(T value)
		{
			Value = value;
		}
	}
	public readonly struct ResultError<T>
	{
		public T Value { get; }

		public ResultError(T value)
		{
			Value = value;
		}
	}
}
