create table MembershipPayments(
	PaymentId int not null references Payments(PaymentId),
	CustomerMembershipId int not null references CustomerMemberships(CustomerMembershipId),
	primary key (PaymentId, CustomerMembershipId)
)