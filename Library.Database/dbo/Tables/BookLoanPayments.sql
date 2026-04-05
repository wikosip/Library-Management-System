create table BookLoanPayments(
	BookLoanId int not null references BookLoans(BookLoanId),
	PaymentId int not null references Payments(PaymentId),
	primary key(BookLoanId, PaymentId)
)