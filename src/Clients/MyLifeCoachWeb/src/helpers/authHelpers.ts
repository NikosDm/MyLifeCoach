import type { User } from "oidc-client-ts";

export default function getUserClaimByName<Type>(
  user: User | null,
  claimName: string,
): Type | null {
  if (!user || !user.profile) {
    return null;
  }
  const profile = user.profile as Record<string, Type>;
  const claim = profile[claimName];
  return claim || null;
}
