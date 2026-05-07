import { render, screen } from '@testing-library/react';
import { expect, test } from 'vitest';
import { LoginComponent }  from '../components/Login'; // Ajuste o caminho conforme seu projeto

test('deve renderizar os campos de email e senha', () => {
  render(<Login />);

  const emailInput = screen.getByLabelText(/email/i);
  const senhaInput = screen.getByLabelText(/senha/i);

  expect(emailInput).toBeDefined();
  expect(senhaInput).toBeDefined();
});
