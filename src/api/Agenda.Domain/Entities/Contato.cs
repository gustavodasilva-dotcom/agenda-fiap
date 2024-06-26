﻿using Agenda.Common.Enums;

namespace Agenda.Domain.Entities;

public class Contato : IEquatable<Contato>
{
    private Contato()
    {
    }

    private Contato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DDD = ddd;
    }

    public int Id { get; private set; }

    public string Nome { get; private set; }

    public string Telefone { get; private set; }

    public string Email { get; private set; }

    public DDDs DDD { get; private set; }

    private IEnumerable<Contato> ObterValoresAtomicos()
    {
        yield return this;
    }

    public static Contato CriarContato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
    {
        var contato = new Contato(
            nome.Trim(),
            telefone.Trim(),
            email.Trim(),
            ddd);

        return contato;
    }

    public Contato AtualizarContato(
        string nome,
        string telefone,
        string email,
        DDDs ddd)
    {
        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Email = email.Trim();
        DDD = ddd;

        return this;
    }

    public bool Equals(Contato? other)
    {
        return other is not null
            && Nome.Trim().Equals(other.Nome.Trim())
            && Telefone.Trim().Equals(other.Telefone.Trim())
            && Email.Trim().Equals(other.Email.Trim())
            && DDD.Equals(other.DDD);
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return ObterValoresAtomicos()
            .Aggregate(
                default(int),
                HashCode.Combine);
    }
}
