public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder
            .HasOne(u => u.Restaurateur)
            .WithOne()
            .HasForeignKey<User>("RestaurateurId")
            .OnDelete(DeleteBehavior.Restrict);
    }